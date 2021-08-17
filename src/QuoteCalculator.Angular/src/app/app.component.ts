import { Component } from '@angular/core';

@Component({
  selector: 'qc-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  pageTitle: string = 'Quote Calculator';
  copyrightYear: number = new Date().getFullYear();
}
