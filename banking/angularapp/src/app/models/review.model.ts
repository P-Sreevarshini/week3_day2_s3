import { User } from "./user.model";

export class Review {
    ReviewId: number;
    UserId: number;
    ReviewText: string;
    DatePosted: Date;
    Rating: number;
    user?: User; 
}
