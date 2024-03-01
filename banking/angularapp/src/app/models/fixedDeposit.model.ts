import { User } from "./user.model";

export class FixedDeposit {
    FixedDepositId: number;
    Amount: number;
    tenureMonths: number;
    interestRate: number;
    user?: User; 
  }