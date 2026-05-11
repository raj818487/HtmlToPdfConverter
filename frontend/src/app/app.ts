import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PdfService, GeneratePdfRequest } from './services/pdf.service';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  htmlContent = `<div class="header">Lab Report</div>
<div class="divider"></div>
<div class="section-title">Patient Details</div>
<div class="row"><div class="label">Patient Name:</div><div class="value">John Doe</div></div>
<div class="row"><div class="label">Gender:</div><div class="value">Male</div></div>
<div class="row"><div class="label">DOB:</div><div class="value">01-Jan-1990</div></div>
<div class="divider"></div>
<div class="section-title">Test Result Details</div>
<table>
  <thead><tr><th>Test Name</th><th>Result</th><th>Unit</th><th>Reference Range</th></tr></thead>
  <tbody>
    <tr><td>Hemoglobin</td><td>13.5</td><td>g/dL</td><td>13.0 - 17.0</td></tr>
    <tr><td>WBC</td><td>6500</td><td>cells/cumm</td><td>4000 - 11000</td></tr>
  </tbody>
</table>
<div class="footer">This is a system generated report.</div>`;

  pageSize = 'A4';
  orientation = 'Portrait';
  marginTop = '20px';
  marginBottom = '20px';
  marginLeft = '20px';
  marginRight = '20px';
  printBackground = true;
  isLoading = false;
  errorMessage = '';

  constructor(private readonly pdfService: PdfService) {}

  generatePdf() {
    this.errorMessage = '';
    const request: GeneratePdfRequest = {
      htmlContent: this.htmlContent,
      pageSize: this.pageSize,
      orientation: this.orientation,
      marginTop: this.marginTop,
      marginBottom: this.marginBottom,
      marginLeft: this.marginLeft,
      marginRight: this.marginRight,
      printBackground: this.printBackground
    };

    this.isLoading = true;

    this.pdfService.generatePdf(request).subscribe({
      next: (blob) => {
        this.downloadFile(blob, 'generated-report.pdf');
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'PDF generation failed. Verify API is running and HTTPS certificate is trusted.';
        this.isLoading = false;
      }
    });
  }

  private downloadFile(blob: Blob, fileName: string) {
    const url = window.URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = fileName;
    anchor.click();
    window.URL.revokeObjectURL(url);
  }
}
