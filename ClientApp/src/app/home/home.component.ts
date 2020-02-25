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

  ngOnInit(): void {
    this.formGroup = new FormGroup({
      username: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      url: new FormControl('', Validators.required)
    });
  }

  onSubmit(){
    let url = document.location.origin + '/api/sendrssdata'
    let rssData = <IRssData> {
      username: this.formGroup.value.username,
      name: this.formGroup.value.name,
      url: this.formGroup.value.url
    }
    this.http.post(url, rssData).subscribe(result => this.sent)
  }

  onInput(){
    if (this.sent) this.sent = false
  }
}
