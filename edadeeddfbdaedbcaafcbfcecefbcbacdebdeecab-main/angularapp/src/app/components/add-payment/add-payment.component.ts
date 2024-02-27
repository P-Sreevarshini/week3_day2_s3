import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Course } from 'src/app/models/course.model';
import { ActivatedRoute, Router } from '@angular/router';
import { PaymentService } from 'src/app/services/payment.service';

@Component({
  selector: 'app-add-payment',
  templateUrl: './add-payment.component.html',
  styleUrls: ['./add-payment.component.css']
})
export class AddPaymentComponent implements OnInit {
  paymentData: any = {
    userId: '', // Populate with the actual user ID
    courseId: 0,
    totalAmount: 0,
    status:'PENDING',
    // course: { courseID: 0, courseName: '', description: '', duration: '', cost: 0 }, // Adjust based on your Course model
    modeOfPayment: '',
    paymentDate: ''
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private paymentService: PaymentService
  ) {}
  showConfirmation: boolean = false;

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      // this.paymentData.userId = params['userId']; // Populate with the actual user ID
      this.paymentData.userId = localStorage.getItem('userId');
      this.paymentData.totalAmount = params['cost'];
      this.paymentData.courseId = params['courseID'];
      this.paymentData.course = { ...params }; // Spread the query parameters into the course object
      console.log('Payment Data:', this.paymentData);
      const role = localStorage.getItem('userId')
      console.log(role);
      
    });
  }

  openConfirmation(): void {
    this.showConfirmation = true;
  }

  closeConfirmation(): void {
    this.showConfirmation = false;
  }

  submitPayment() {
    this.paymentService.submitPayment(this.paymentData).subscribe(
      (response) => {
        // Handle successful payment submission
        console.log('Payment submitted:', response);
        // Optionally, you can redirect the user after successful payment
        this.router.navigate(['/student/dashboard']);
      },
      (error) => {
        // Handle payment submission error
        console.error('Error submitting payment:', error);
      }
    );
  }

}