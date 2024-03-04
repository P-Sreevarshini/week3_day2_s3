import { User } from "./user.model";

export class Review {
    ReviewId: number;
    UserId: number;
    Body: string;
    Rating: number;
    DateCreated: Date;
    user?: User; 
}
