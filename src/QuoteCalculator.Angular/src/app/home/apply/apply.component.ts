import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { QuoteDetailModel } from '../../api/models';
import { QuoteService } from '../../api/services';

@Component({
  selector: 'qc-apply',
  templateUrl: './apply.component.html',
  styleUrls: ['./apply.component.css']
})
export class ApplyComponent implements OnInit {
  showComputation: boolean = false;
  model: QuoteDetailModel = new QuoteDetailModel();

  constructor(private router: Router,
    private toastr: ToastrService,
    public quoteService: QuoteService
  ) { }

  ngOnInit(): void {
    this.model = history.state.model as QuoteDetailModel ?? new QuoteDetailModel();
  }

  onToggleComputation(): void {
    this.showComputation = !this.showComputation;
  }

  onApplyLoan(): void {
    this.model.quoteSchedules = null;
    this.quoteService.putQuote(this.model).subscribe(
      result => {
        this.router.navigate(['quote']).then(() => {
          this.toastr.success(`Thank you ${this.model.fullName}! Your loan application is under review.`, 'Loan Application');
        });
      },
      error => { console.log(error); }
    );
  }

  onEditQuote(): void {
    this.model.quoteSchedules = null;
    this.router.navigate(['quote'], { state: { model: this.model, isEdit: true } });
  }
}
