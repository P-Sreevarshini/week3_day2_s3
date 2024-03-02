import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Review } from 'src/app/models/review.model'; // Assuming you have the Review model
import { ResortService } from 'src/app/services/resort.service';

@Component({
  selector: 'app-add-review',
  templateUrl: './add-review.component.html',
  styleUrls: ['./add-review.component.css'],
})
export class AddReviewComponent implements OnInit {
  addReviewForm: FormGroup;
  errorMessage = '';
  resorts: any[] = [];

  constructor(private fb: FormBuilder, private resortService: ResortService, private router: Router) {
    this.addReviewForm = this.fb.group({
      userName: [localStorage.getItem('userName'), Validators.required],
      Body: ['', Validators.required],
      Rating: ['', Validators.required],
      DateCreated: [this.getCurrentDate(), Validators.required],
    });
  }

  ngOnInit() {
    // Initialize any data or subscribe to necessary observables
    console.log(localStorage.getItem('userName'))
    this.getAllResorts();
  }

  getAllResorts() {
    this.resortService.getAllResorts().subscribe(
      (data: any) => {
        this.resorts = data;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  getCurrentDate(): string {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
    const day = ('0' + currentDate.getDate()).slice(-2);

    return `${year}-${month}-${day}`;
  }

  onSubmit(): void {
    if (this.addReviewForm.valid) {
      const newReview = this.addReviewForm.value;
      const requestObj: Review = {
        userId: Number(localStorage.getItem('userId')),
        Body: newReview.body,
        Rating: newReview.rating,
        DateCreated: newReview.dateCreated
      };
      console.log(requestObj)

      this.resortService.addReview(requestObj).subscribe(
        (response) => {
          console.log('Review added successfully', response);
          // Handle success, e.g., navigate to a different page
          this.router.navigate(['/customer/view/review']);
          this.addReviewForm.reset(); // Reset the form
        },
        (error) => {
          console.error('Error adding review', error);
          // Handle error, display an error message, etc.
        }
      );
    } else {
      this.errorMessage = 'All fields are required';
    }
  }
}
