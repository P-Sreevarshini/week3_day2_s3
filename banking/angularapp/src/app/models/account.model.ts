import { User } from "./user.model";

export class Account {
    AccountId: number;
    UserId: number; 
    Balance: number;
    AccountType: string;
    User?: User; // Optional, assuming User is also a model
  }