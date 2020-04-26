using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RSSSender.Models;
using RSSSender.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Text;
using RSSSender.ViewModels;

namespace RSSSender.Controllers
{
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly IRssStoreService rssStoreService;
        private readonly IConfiguration configuration;
        private readonly IRssReaderService rssReaderService;

        public ApiController(
            IRssStoreService rssStoreService, 
            IConfiguration configuration,
            IRssReaderService rssReaderService)
        {
            this.rssStoreService = rssStoreService;
            this.configuration = configuration;
            this.rssReaderService = rssReaderService;
        }

        [HttpPost("saverssdata")]
        public async Task<IActionResult> SaveRssData([FromBody]RssData rssData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await rssStoreService.SaveRssDataAsync(rssData);

            return Ok();
        }

        [HttpPost("sendnotification")]
        public async Task<IActionResult> PostMessage(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return Ok("Empty list");

            var allRss = await rssStoreService.GetAllItemsAsync();
            var filtredRss = allRss?.Where(i => i.Email == emailAddress);

            var apiKey = configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);

            var rssBodyBuilder = new StringBuilder();
            var rssLinks = new List<string>();
            filtredRss?.ToList().ForEach(rss =>
            {
                var rssResult = rssReaderService.GetRss(rss.Url);
                rssBodyBuilder.Append(rssResult);
                rssLinks.Add(rss.Url);
            });


            var from = new EmailAddress("protheus@tlen.pl");
            var tos = new List<EmailAddress> { new EmailAddress(emailAddress, string.Empty) };
            var subject = "Rss message";
            var rssBody = rssBodyBuilder.ToString();
            var htmlContent = $"<strong>{rssBody}</strong>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
                from, tos, subject, "", htmlContent, false);
            await client.SendEmailAsync(msg);

            var rssResponse = new RssResponseViewModel
            {
                Links = rssLinks,
                RssBody = rssBody
            };
            return Ok(rssResponse);
        }
    }
}