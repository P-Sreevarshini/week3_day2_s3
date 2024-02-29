import { User } from "./user.model";

export class Account {
    accountId: number;
    userId: number; // Foreign key referencing User
    balance: number;
    accountType: string;
    user?: User; // Optional, assuming User is also a model
  }