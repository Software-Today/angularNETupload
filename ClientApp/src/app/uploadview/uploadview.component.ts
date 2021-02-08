import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'

@Component({
  selector: 'app-uploadview',
  templateUrl: './uploadview.component.html',
  styleUrls: ['./uploadview.component.css']
})
export class UploadviewComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

}
