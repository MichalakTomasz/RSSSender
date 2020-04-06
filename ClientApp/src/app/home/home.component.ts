import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { IRssData } from '../interfaces/i-rss-data';

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

  ngOnInit(): void {
    this.formGroup = new FormGroup({
      url: new FormControl('', Validators.required),
      email: new FormControl('', { 
        validators: [Validators.email, Validators.required],
        updateOn: 'blur'})
    });
  }

  onSubmit(){
    let url = document.location.origin + '/api/saverssdata'
    let rssData = <IRssData> {
      url: this.formGroup.value.url,
      email: this.formGroup.value.email
    }
    this.http.post<boolean>(url, rssData).subscribe(result => this.sent = result)
  }

  onInput(){
    if (this.sent) this.sent = false
  }
}
