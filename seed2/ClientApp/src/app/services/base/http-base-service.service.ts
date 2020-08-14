import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ServiceSettings } from '../../service-settings'
import { LoopbackFilter } from "./loopbackfilter";
import { Observable, of } from 'rxjs';


export class HttpBaseServiceService {
  protected headers = new HttpHeaders();
  private apiUrl = "";
  private configuration: ServiceSettings;
  public endpoint;
  public endpointFilterAction;

  protected filter :LoopbackFilter;

  constructor(protected httpClient: HttpClient, endpoint, endpointFilterAction,
      configuration: ServiceSettings) {
    //this.apiUrl = configuration.baseURL;
      this.apiUrl = "";
      this.endpoint = endpoint;
      this.configuration = configuration;
      this.endpointFilterAction = endpointFilterAction;
      this.headers = this.headers.set('Content-Type', 'application/json');
      this.headers = this.headers.set('Accept', 'application/json');
      
      // this.headers = this.headers.set('Authorization', 'Bearer ' + this.getJWToken());
    }

    /*getUrl(): Observable<string> {
        this.httpClient.get(window.location.origin + '/api/ServiceSettings').subscribe(res => {
            var aux: any = JSON.parse(JSON.stringify(res));
            this.apiUrl = aux.baseURL;
            return of(this.apiUrl);
        });
    }*/


    getAll<T>() {
        /*if (!this.apiUrl || this.apiUrl == "") {
            console.log('entro al subscribe de getAll');
            this.httpClient.get(window.location.origin + '/api/ServiceSettings').subscribe(res => {
                var aux: any = JSON.parse(JSON.stringify(res));
                this.apiUrl = aux.baseURL;
                if (!this.endpoint || this.endpoint == "" || !this.endpointFilterAction || this.endpointFilterAction == "") {
                    console.error("HttpBaseServiceService error. No se ha establecido el actioncontroller.");
                    return;
                }
                const mergedUrl = this.apiUrl + this.endpoint + "/" + this.endpointFilterAction + "?loopback=" + JSON.stringify(this.filter);
                return this.httpClient.get<T>(mergedUrl, { headers: this.headers, observe: 'response' });
            });
        }
        else {
            if (!this.endpoint || this.endpoint == "" || !this.endpointFilterAction || this.endpointFilterAction == "") {
                console.error("HttpBaseServiceService error. No se ha establecido el actioncontroller.");
                return;
            }
            const mergedUrl = this.apiUrl + this.endpoint + "/" + this.endpointFilterAction + "?loopback=" + JSON.stringify(this.filter);
            return this.httpClient.get<T>(mergedUrl, { headers: this.headers, observe: 'response' });
        }*/
        if (!this.endpoint || this.endpoint == "" || !this.endpointFilterAction || this.endpointFilterAction == "") {
            console.error("HttpBaseServiceService error. No se ha establecido el actioncontroller.");
            return;
        }
      const mergedUrl = this.apiUrl + this.endpoint + "/" + this.endpointFilterAction + "?loopback=" + JSON.stringify(this.filter);
      return this.httpClient.get<T>(mergedUrl, { headers: this.headers, observe: 'response' });
        
    }

  getSingle<T>(id: number) {
    return this.httpClient.get<T>(`${this.apiendpoint()}/${id}`);
  }
  add<T>(toAdd: T) {
    return this.httpClient.post<T>(this.endpoint, toAdd, { headers: this.headers });
  }
  update<T>(url: string, toUpdate: T) {
    return this.httpClient.put<T>(url,
      toUpdate,
      { headers: this.headers });
  }
  delete(url: string) {
    return this.httpClient.delete(url);
  }

  deleteBody(url: string) {
    // return this.httpClient.delete(url);
    return this.httpClient.request('delete', url, { body: this.filter });
  }

  prepareGetAllFilter() {
      this.filter.limit = this.configuration.DefaultPageSize;
  }

  setPagination(skip: number, limit: number) {
    this.filter.skip = skip;
    this.filter.limit = limit;
  }

    public apiendpoint() {
        if (!this.apiUrl || this.apiUrl == "") {
            this.httpClient.get(window.location.origin + '/api/ServiceSettings').subscribe(res => {
                var aux: any = JSON.parse(JSON.stringify(res));
                this.apiUrl = aux.baseURL;
                return this.apiUrl + this.endpoint;
            });
        } else {
            return this.apiUrl + this.endpoint;
        }
        
    }

  getJWToken():string{return localStorage.getItem('jwt');}

  getSortingValue(previousorder, currentorder, newValue):string{
    if (currentorder == previousorder && currentorder == newValue) {
      return undefined;
    }
    if (!previousorder && currentorder == newValue) {
      return newValue;
    }
    if (previousorder != newValue || currentorder != newValue) {
      return newValue;
     
    }
    if (currentorder == newValue) {
      return newValue;
    }
  }

  getSortingAscending(previousorder, currentorder, newValue):any{
    if (currentorder == previousorder && currentorder == newValue) {      
      return undefined;      
    }
    if (!previousorder && currentorder == newValue) {      
      return false;      
    }
    if (previousorder != newValue || currentorder != newValue) {      
      return true;
      
    }
    if (currentorder == newValue) {      
      return false;      
    }
  }

  getPrevious(previousorder, currentorder, newValue):any{
    if (currentorder == previousorder && currentorder == newValue) {
      return undefined;
    }
    if (!previousorder && currentorder == newValue) {
      return currentorder;
    }
    if (previousorder != newValue || currentorder != newValue) {
      return currentorder;
    }
    if (currentorder == newValue) {     
      return currentorder;     
    }
  }

  getCurrent(previousorder, currentorder, newValue):any{
    if (currentorder == previousorder && currentorder == newValue) {
      return undefined;
    }
    if (!previousorder && currentorder == newValue) {
      return newValue;
    }
    if (previousorder != newValue || currentorder != newValue) {
      return newValue;      
    }
    if (currentorder == newValue) {      
      return newValue;      
    }
  }
}
