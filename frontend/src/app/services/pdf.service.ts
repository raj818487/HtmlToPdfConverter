import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface GeneratePdfRequest {
  htmlContent: string;
  pageSize: string;
  orientation: string;
  marginTop: string;
  marginBottom: string;
  marginLeft: string;
  marginRight: string;
  printBackground: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class PdfService {
  private readonly apiUrl = 'https://localhost:5001/api/pdf/generate';

  constructor(private readonly http: HttpClient) {}

  generatePdf(request: GeneratePdfRequest) {
    return this.http.post(this.apiUrl, request, { responseType: 'blob' });
  }
}
