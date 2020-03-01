using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RSSSender.Models;
using RSSSender.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RSSSender.Controllers
{
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly IRssStoreService rssStoreService;
        private readonly IConfiguration configuration;

        public ApiController(IRssStoreService rssStoreService, IConfiguration configuration)
        {
            this.rssStoreService = rssStoreService;
            this.configuration = configuration;
        }

        [HttpPost("sendrssdata")]
        public async Task<IActionResult> SendRssData([FromBody] RssData rssData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await rssStoreService.SaveRssDataAsync(rssData);

            return Ok(result);
        }

        [HttpGet("sendnotification")]
        public async Task<IActionResult> PostMessage()
        {
            var apiKey = configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("vlad3dracula@gmail.com", "Example User 1");
            var tos = new List<EmailAddress>
            {
              new EmailAddress("protheus@o2.com", "protheus")
            };

            var subject = "Hello world email from Sendgrid ";
            var htmlContent = "<strong>Hello world with HTML content</strong>"; 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
            var response = await client.SendEmailAsync(msg);

            return Ok(true);
        }
    }
}