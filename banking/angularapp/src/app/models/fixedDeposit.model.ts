import { User } from "./user.model";

export class FixedDeposit {
    fixedDepositId: number;
    amount: number;
    tenureMonths: number;
    interestRate: number;
    user?: User; 
  }