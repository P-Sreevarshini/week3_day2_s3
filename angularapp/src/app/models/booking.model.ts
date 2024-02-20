import { Resort } from "./resort.model";
import { User } from "./user.model";

export class Booking {
  bookingId?: number;
  noOfPersons: number;
  fromDate: Date;
  toDate: Date;
  status: string;
  totalPrice: number;
  address: string;
  userId?: number; // Nullable foreign key
  user?: User; // Nullable navigation property
  resortId?: number; // Nullable foreign key
  resort?: Resort; // Nullable navigation property
}

//one