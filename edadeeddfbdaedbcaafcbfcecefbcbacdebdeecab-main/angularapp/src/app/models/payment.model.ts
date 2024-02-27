export interface Payment {
    paymentId: number;
    courseId: number;
    userId: number;
    status: string;
    totalAmount: number;
    paymentDate: Date;
    modeOfPayment: string;
  }
  