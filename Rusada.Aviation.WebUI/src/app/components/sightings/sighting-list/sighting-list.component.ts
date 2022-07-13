import { AfterViewInit, Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { ADTSettings } from 'angular-datatables/src/models/settings';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { DataTablesResponse } from 'src/app/common/classes/data-table-response';
import { RusadaHttpClientService } from 'src/app/_helpers/rusada-http-client.service';
import { Sighting } from 'src/app/_models/sighting';
import { SightingDetailComponent } from '../sighting-detail/sighting-detail.component';

@Component({
  selector: 'app-sighting-list',
  templateUrl: './sighting-list.component.html',
  styleUrls: ['./sighting-list.component.css']
})
export class SightingListComponent implements OnInit, AfterViewInit {
  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective;

  dtOptions: DataTables.Settings = {};
  sightingList: Sighting[];
  dtTrigger: Subject<any> = new Subject<any>();

  modalRef: BsModalRef;

  searchText: string;
  searchBy: string;

  constructor(private rusadaHttpClientService: RusadaHttpClientService<DataTablesResponse>,
    private renderer: Renderer2, private elRef: ElementRef, private modalService: BsModalService) { }

  ngOnInit(): void {
    this.setDataTableOptions();
  }

  setDataTableOptions() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 2,
      serverSide: true,
      processing: true,
      searching: false,
      ordering: false,
      ajax: (dataTablesParameters: any, callback) => {
        this.rusadaHttpClientService.postData('v1/sighting/all', dataTablesParameters).subscribe(res => {
          console.log(res);
          callback({
            recordsTotal: res.recordsTotal,
            recordsFiltered: res.recordsFiltered,
            data: res.data
          });
        });

      },
      columns: [{ data: 'make' },
      { data: 'model' },
      { data: 'registration' },
      { data: 'location' },
      {
        data: 'date',
        render: function (data, type, row) {
          return moment(data).format('YYYY-MM-DD hh:mm a');
        }
      },
      {
        data: '',
        orderable: false,
        render: function (data, type, row) {
          return '<button id="detail_' + row.id + '" class="dt-button me-2" (click)="details()" title="Details" name="details">' +
            '<i class="fa fa-info" aria-hidden="true" id="detail_' + row.id + '" name="details"></i>' +
            '</button>';
        }
      },
      {
        data: '',
        orderable: false,
        render: function (data, type, row) {
          return '<button id="edit_' + row.id + '" class="dt-button me-2" (click)="edit()" title="Edit" name="edit">' +
            '<i class="fa fa-edit" aria-hidden="true" id="edit_' + row.id + '" name="edit"></i>' +
            '</button>';
        }
      },
      {
        data: '',
        orderable: false,
        render: function (data, type, row) {
          return '<button id="delete_' + row.id + '" class="dt-button" (click)="delete()" title="Delete" name="delete">' +
            '<i class="fa fa-trash" aria-hidden="true" id="delete_' + row.id + '" name="delete"></i>' +
            '</button>';
        }
      }]
    };
  }

  details(id: number) {
    this.modalRef = this.modalService.show(SightingDetailComponent,);
    this.modalRef.content.isReadonly = true;
    this.modalRef.content.loadDetailData(id);
  }

  edit(id: number) {
    this.modalRef = this.modalService.show(SightingDetailComponent,);
    this.modalRef.content.isReadonly = false;
    this.modalRef.content.loadDetailData(id);
  }

  delete(id: number) {
    if (confirm('Confirm delete sighting?')) {
      this.rusadaHttpClientService.deleteData('v1/sighting/' + id).subscribe(res => {
        this.rerender();
      });
    }
  }

  ngAfterViewInit(): void {
    this.renderer.listen('document', 'click', (event) => {
      if (event.target.hasAttribute("name")) {
        var action = event.target.attributes['name'].value;
        var dataId = event.target.attributes['id'].value.split('_')[1];

        switch (action) {
          case "details":
            this.details(dataId);
            break;
          case "edit":
            this.edit(dataId);
            break;
          case "delete":
            this.delete(dataId);
            break;
        }
      }
    });
    this.dtTrigger.next(0);
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();
      this.dtTrigger.next(0);
    });
  }

  search() {
    if (this.searchBy !== null && this.searchText !== null && this.searchText !== '') {
      this.dtOptions.ajax = (dataTablesParameters: any, callback) => {
        let searchCol = dataTablesParameters.columns.filter((a: any)=> a.data == this.searchBy)[0];
        if(searchCol !== null){
          searchCol.search.value = this.searchText;
        }
        this.rusadaHttpClientService.postData('v1/sighting/all', dataTablesParameters).subscribe(res => {
          console.log(res);
          callback({
            recordsTotal: res.recordsTotal,
            recordsFiltered: res.recordsFiltered,
            data: res.data
          });
        });
      };
      this.rerender();
    }
  }
}
