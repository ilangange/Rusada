import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RusadaHttpClientService<T> {

  constructor(private http: HttpClient) { }

  getData(url: string) {
    return this.http.get<T>(environment.base_api + url);
  }

  postData(url: string, item: T) {
    return this.http.post<T>(environment.base_api + url, item);
  }

  deleteData(url: string) {
    return this.http.delete<T>(environment.base_api + url);
  }

  putData(url: string, item: T) {
    return this.http.put<T>(environment.base_api + url, item);
  }
}
