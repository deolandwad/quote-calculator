import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { QuoteDetailModel } from '../models';

@Injectable({
  providedIn: 'root'
})
export class QuoteService {

  constructor(private http: HttpClient) { }

  readonly baseURL = 'https://localhost:44384/api/Quote'

  postQuote(model: QuoteDetailModel) {
    return this.http.post(this.baseURL, model);
  }

  putQuote(model: QuoteDetailModel) {
    return this.http.put(`${this.baseURL}/${model.id}`, model);
  }
}
