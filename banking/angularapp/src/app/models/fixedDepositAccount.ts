    import { FixedDeposit } from "./fixedDeposit.model";
    import { User } from "./user.model";

    export class FDAccount {
        FDAccountId: number;
        UserId: number;
        Status: string;
        FixedDepositId: number;
        User?: User; 
        FixedDeposit?: FixedDeposit; 
    }
        