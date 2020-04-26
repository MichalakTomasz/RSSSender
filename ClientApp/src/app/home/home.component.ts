import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { IRssData } from '../interfaces/i-rss-data';
import { IRssResponse } from '../interfaces/i-rss-response';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private http: HttpClient) {}

  formGroup: FormGroup
  sent = false
  rss: String
  rssRssponse : IRssResponse

  ngOnInit(): void {
    this.formGroup = new FormGroup({
      url: new FormControl('', Validators.required),
      email: new FormControl('', { 
        validators: [Validators.email, Validators.required],
        updateOn: 'blur'})
    });
  }

  onSubmit() {
    let url = document.location.origin + '/api/sendnotification'
    this.http.post<boolean>(url, this.formGroup.value.email)
    .subscribe(result => this.sent = result)
  }

  onSave() {
    let url = document.location.origin + '/api/saverssdata'
    let rssData = <IRssData> {
      url: this.formGroup.value.url,
      email: this.formGroup.value.email
    }
    this.http.post<IRssResponse>(url, rssData).subscribe(result =>
      this.rssRssponse = result)
  }

  onInput(){
    if (this.sent) this.sent = false
  }
}
