import { User } from "./user.model";

export class Review {
    reviewId: number;
    userId: number;
    reviewText: string;
    datePosted: Date;
    rating: number;
    user?: User; 
  }
