
import { QuoteScheduleModel } from './quote-schedule-model';
export class QuoteDetailModel {
  id?: number;
  firstName?: string;
  lastName?: string;
  email?: string;
  mobileNumber?: string;
  terms?: number;
  interestRate?: number;
  financeAmount?: number;
  repaymentAmount?: number;
  totalInterest?: number;
  fullName?: null | string;
  quoteSchedules?: null | Array<QuoteScheduleModel>;
}
