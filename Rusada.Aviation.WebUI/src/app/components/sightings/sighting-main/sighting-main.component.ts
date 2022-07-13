import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NewSightingComponent } from '../new-sighting/new-sighting.component';

@Component({
  selector: 'app-sighting-main',
  templateUrl: './sighting-main.component.html',
  styleUrls: ['./sighting-main.component.css']
})
export class SightingMainComponent implements OnInit {
  modalRef: BsModalRef;

  constructor(private modalService: BsModalService) { }

  ngOnInit(): void {
  } 

  newSighting() {
    this.modalRef = this.modalService.show(NewSightingComponent);
  }

}
