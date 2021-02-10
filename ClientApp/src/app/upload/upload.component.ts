import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { filter } from 'rxjs/operator/filter';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  public fileType: string;
  public typeChecker: string;
  public fMiniSize: number;
  public fMaxSize: number;
  public progress: number;
  public message: string;
  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  upload(files) {
    if (files.length === 0)
      return;
    if (this.fMaxSize === 0){
      this.fMaxSize = 4096;
    }
    if (this.fMiniSize === 0){
      this.fMiniSize = 2048;
    }
    if (files.length > 0) { 
              for (var i = 0; i <= files.length - 1; i++) { 
  
                const fsize = files.item(i).size; 
                const file = Math.round((fsize / 1024)); 
                // The size of the file. 
                if (file >= this.fMaxSize) { 
                    alert( 
                      "File too Big, please select a file less than 4mb"); 
                      return;
                } else if (file < this.fMiniSize) { 
                    alert( 
                      "File too small, please select a file greater than 2mb");
                      return;
                } else { 
                    this.message = 'Upload file size:' + file + 'KB'; 
                } 
            } 
        } 

    const formData = new FormData();
    for (let file of files)
      formData.append(file.name, file);
    console.log(formData);

    const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response)
        this.message = event.body.toString();
    });
  }

  change(type: string): void {
    switch (type) {
      case 'video':
        this.typeChecker = '.mp4, .mov';
        break;
      case 'pdf':
        this.typeChecker = '.pdf';
        break;
      case 'image':
        this.typeChecker = '.jpg, .png';
    }
  }

}
