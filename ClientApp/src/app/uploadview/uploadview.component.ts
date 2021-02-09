import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'

@Component({
  selector: 'app-uploadview',
  templateUrl: './uploadview.component.html',
  styleUrls: ['./uploadview.component.css']
})
export class UploadviewComponent implements OnInit {

  public fproperties: FileProperties[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<FileProperties[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.fproperties = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}

interface FileProperties {
  filename: string;
  filesize: number;
  uploaddate: string;
}