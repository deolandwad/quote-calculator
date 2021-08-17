import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { QuoteComponent } from './home/quote/quote.component';
import { ApplyComponent } from './home/apply/apply.component';

@NgModule({
  declarations: [
    AppComponent,
    QuoteComponent,
    ApplyComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: 'quote', component: QuoteComponent },
      { path: 'apply', component: ApplyComponent },
      { path: '', redirectTo: 'quote', pathMatch: 'full' },
      { path: '**', redirectTo: 'quote', pathMatch: 'full' }
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
