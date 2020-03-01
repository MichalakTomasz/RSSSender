import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-sender',
  templateUrl: './sender.component.html',
  styleUrls: ['./sender.component.css']
})
export class SenderComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  sent: boolean

  onSend(){
    let url = document.location.origin + '/api/sendnotification'
    this.http.get<boolean>(url).subscribe(result => this.sent = result)
  }
}
