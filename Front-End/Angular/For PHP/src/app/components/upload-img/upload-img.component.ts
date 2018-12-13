import { Component, ViewChild, ElementRef,Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-upload-img',
  templateUrl: './upload-img.component.html',
  styleUrls: ['./upload-img.component.css']
})
export class UploadImgComponent {

  //----------------PROPERTIES-------------------

  //reference to element in html that has local variable named 'inputFile'
  // (local variable in html is declared with # char)
  @ViewChild('inputFile')
  myInputVariable: ElementRef;

  
  //reference to element in html that has local variable named 'imageUpload'
  // (local variable in html is declared with # char)
  @ViewChild('imageUpload')
  imageUpload: ElementRef;

  @Input()
  locationUrl: string = null;

  @Output()
  eventImage: EventEmitter<any>;
 
  imageControl: FormControl;
  imageName: string = '';

  //----------------CONSTRUCTOR------------------

  constructor() {
    this.eventImage = new EventEmitter<any>();
    this.imageControl = new FormControl('');
  }

  //----------------METHODS-------------------

  showPreviewImage(event: any): void {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.locationUrl = event.target.result;
      }
      reader.readAsDataURL(event.target.files[0]);
      this.imageName = event.target.files[0].name;
    }
    this.eventImage.emit(event.target.files[0]);
  }

  triggerInputFileEvent() {
    this.myInputVariable.nativeElement.click();
  }
  removeUpload() {
    this.locationUrl = null;
    this.imageControl.setValue(null);
    this.eventImage.emit(null);
  }
}