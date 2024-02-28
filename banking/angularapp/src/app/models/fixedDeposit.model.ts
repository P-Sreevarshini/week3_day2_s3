import { User } from "./user.model";

export class FixedDeposit {
    fixedDepositId: number;
    userId: number;
    amount: number;
    tenureMonths: number;
    interestRate: number;
    startDate: Date;
    user?: User; 
  }