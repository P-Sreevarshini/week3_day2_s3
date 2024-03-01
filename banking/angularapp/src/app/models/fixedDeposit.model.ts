import { User } from "./user.model";

export class FixedDeposit {
    FixedDepositId: number;
    amount: number;
    tenureMonths: number;
    interestRate: number;
  }