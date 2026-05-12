# HtmlToPdfConverter

Angular + .NET 10 + Playwright based HTML-to-PDF converter.

## Structure

- `/PdfGenerator.Api` - ASP.NET Core 10 API with Playwright PDF generation
- `/frontend` - Angular frontend with HTML editor, PDF options, and download

## Backend setup

```bash
cd PdfGenerator.Api
dotnet restore
dotnet build
pwsh bin/Debug/net10.0/playwright.ps1 install
dotnet run
```

API endpoint:

- `POST https://localhost:7237/api/pdf/generate`

## Frontend setup

```bash
cd frontend
npm install
npm start
```

Frontend runs on `http://localhost:4200`.

## Notes

- Frontend style uses Claude DESIGN.md-inspired tokens (warm canvas, coral CTA, editorial typography).
- Basic script-tag sanitization is applied server-side before PDF rendering.
