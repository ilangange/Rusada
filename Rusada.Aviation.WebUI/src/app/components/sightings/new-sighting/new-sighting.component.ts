import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DlDateTimePickerChange } from 'angular-bootstrap-datetimepicker';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { RusadaHttpClientService } from 'src/app/_helpers/rusada-http-client.service';
import { Sighting } from 'src/app/_models/sighting';

@Component({
  selector: 'app-new-sighting',
  templateUrl: './new-sighting.component.html',
  styleUrls: ['./new-sighting.component.css']
})
export class NewSightingComponent implements OnInit {
  formNewSighting: FormGroup;  

  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private rusadaHttpClientService: RusadaHttpClientService<Sighting>,
    private toastr: ToastrService) {
    this.formNewSighting = this.fb.group({
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

  ngOnInit(): void {
  }

  closeModal() {
    this.modalService.hide(1);
  }

  saveSighting() {
    if (this.formNewSighting.valid) {
      this.formNewSighting.get('date')?.patchValue(new Date(this.formNewSighting.get('date')?.value));
      this.rusadaHttpClientService.postData('v1/sighting', this.formNewSighting.value).subscribe(res => {
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

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.formNewSighting.patchValue({
          fileData: reader.result?.toString()
        });
      };
    }
  }

  onCustomDateChange(event: DlDateTimePickerChange<Date>) {
    this.formNewSighting.patchValue({
      // date: event.value
      date: moment(event.value).format()
    });
  }

}
