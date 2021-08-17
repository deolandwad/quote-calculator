import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { QuoteDetailModel } from '../../api/models';
import { QuoteService } from '../../api/services';

@Component({
  selector: 'qc-quote',
  templateUrl: './quote.component.html',
  styleUrls: ['./quote.component.css']
})
export class QuoteComponent implements OnInit {
  isEdit: boolean = false;
  pageHeading: string | undefined;
  pageButton: string | undefined;
  model: QuoteDetailModel = new QuoteDetailModel();

  constructor(private router: Router,
    private toastr: ToastrService,
    private quoteService: QuoteService
  ) { }

  ngOnInit(): void {
    this.isEdit = history.state.isEdit ?? false;
    this.model = history.state.model as QuoteDetailModel ?? new QuoteDetailModel();
    this.togglePageName();
  }

  onGetQuote(): void {
    this.quoteService.postQuote(this.model).subscribe(
      result => {
        this.router.navigate(['apply'], { state: { model: result as QuoteDetailModel} });
      },
      error => {
        console.log(error);
        this.toastr.error('Unable to validate quote. Please try again.', 'Quote Calculator');        
      }
    );
  }

  private togglePageName(): void {
    this.pageHeading = (this.isEdit) ? 'Update quote' : 'Quote Calculator';
    this.pageButton = (this.isEdit) ? 'Update quote' : 'Calculate quote';
  }
}
