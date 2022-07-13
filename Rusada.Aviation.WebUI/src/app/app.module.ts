import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AppRoutingModule } from './app.routing';
import { AuthService } from './services/auth-service';
import { DataTablesModule } from 'angular-datatables';
import { SightingListComponent } from './components/sightings/sighting-list/sighting-list.component';
import { SightingMainComponent } from './components/sightings/sighting-main/sighting-main.component';
import { NewSightingComponent } from './components/sightings/new-sighting/new-sighting.component';
import { HttpClientInterceptor } from './_helpers/http-client.interceptor';
import { SightingDetailComponent } from './components/sightings/sighting-detail/sighting-detail.component';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from '@danielmoncada/angular-datetime-picker';
import { DlDateTimeDateModule, DlDateTimePickerModule } from 'angular-bootstrap-datetimepicker';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SightingListComponent,
    SightingMainComponent,
    NewSightingComponent,
    SightingDetailComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    DataTablesModule,
    ModalModule.forRoot(),
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    DlDateTimeDateModule,
    DlDateTimePickerModule
  ],
  providers: [
    AuthService,
    BsModalService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpClientInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
