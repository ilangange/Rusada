import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DlDateTimePickerChange } from 'angular-bootstrap-datetimepicker';
import * as moment from 'moment';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ApiResponse } from 'src/app/common/classes/api-response';
import { RusadaHttpClientService } from 'src/app/_helpers/rusada-http-client.service';
import { Sighting } from 'src/app/_models/sighting';

@Component({
  selector: 'app-sighting-detail',
  templateUrl: './sighting-detail.component.html',
  styleUrls: ['./sighting-detail.component.css']
})
export class SightingDetailComponent implements OnInit {
  formSightingDetails: FormGroup;
  public isReadonly: boolean = true;
  imageData: string;

  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private rusadaHttpClientService: RusadaHttpClientService<ApiResponse>,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.formSightingDetails = this.fb.group({
      id: new FormControl('', [Validators.required]),
      make: new FormControl('', [Validators.required]),
      model: new FormControl('', [Validators.required]),
      //registration: ['', Validators.required, Validators.pattern(/([a-zA-Z0-9]{1,2}-[a-zA-Z0-9]{1,5})/g)],
      registration: new FormControl('', [Validators.required]),
      location: new FormControl('', [Validators.required]),
      date: new FormControl('', [Validators.required]),
      file: new FormControl('', []),
      fileData: new FormControl('', [])
    });
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.formSightingDetails.patchValue({
          fileData: reader.result
        });
      };
    }
  }

  closeModal() {
    this.modalService.hide(1);
  }

  updateSighting() {
    if (this.formSightingDetails.valid) {
      this.formSightingDetails.get('date')?.patchValue(new Date(this.formSightingDetails.get('date')?.value));
      this.rusadaHttpClientService.putData('v1/sighting', this.formSightingDetails.value).subscribe(res => {
        if (res !== null) {
          this.toastr.success('New Sighting saved.', 'Success');
          this.modalService.hide(1);
        }
        else {
          this.toastr.error('Error Occured.');
        }
      });
    }
    else {
      this.toastr.error('Invalid Data.');
    }
  }

  public loadDetailData(id: number) {
    this.rusadaHttpClientService.getData('v1/sighting/' + id).subscribe(res => {
      if(res != null){
        this.formSightingDetails.get('id')?.patchValue(res.data.id);
        this.formSightingDetails.get('make')?.patchValue(res.data.make);
        this.formSightingDetails.get('model')?.patchValue(res.data.model);
        this.formSightingDetails.get('registration')?.patchValue(res.data.registration);
        this.formSightingDetails.get('location')?.patchValue(res.data.location);
        this.formSightingDetails.get('date')?.patchValue(moment(res.data.date).format('YYYY-MM-DD hh:mm a'));
        this.formSightingDetails.get('fileData')?.patchValue(res.data.fileData);
        this.imageData = res.data.fileData;
      }
    });
    
  }

  onCustomDateChange(event: DlDateTimePickerChange<Date>) {
    this.formSightingDetails.patchValue({
      // date: event.value
      date: moment(event.value).format()
    });
  }

}
