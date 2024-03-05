import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service';
import { Review } from 'src/app/models/review.model';
import { AuthService } from 'src/app/services/auth.service';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-view-review',
  templateUrl: './view-review.component.html',
  styleUrls: ['./view-review.component.css']
})
export class ViewReviewComponent implements OnInit {
  reviews: Review[] = [];
  userRole: string;

  constructor(private reviewService: ReviewService, private authService: AuthService) { }

  ngOnInit(): void {
    this.userRole = localStorage.getItem('userRole');
    if (this.userRole === 'Admin') {
      this.getAllReviews();
    } else if (this.userRole === 'Customer') {
      this.getReviewsByUserId();
    }
  }

  getAllReviews() {
    if (this.userRole === 'Admin') {

    this.reviewService.getAllReviews().subscribe(
      (data: Review[]) => { 
        this.reviews = data;
      },
      (err) => {
        console.log(err);
      }
    );
  }
  }
  deleteReview(reviewId: number): void {
    const role = localStorage.getItem('userRole');
    const userId = localStorage.getItem('user'); // Retrieve userId from localStorage
    
    if (role !== 'Customer') {
      console.error('Access denied. Only customers can delete reviews.');
      return; // Return early if not a customer
    }
    
    if (!userId) {
      console.error('User ID is undefined.');
      return; // Return early if userId is undefined
    }
  
    this.reviewService.deleteReviewByUserId(parseInt(userId), reviewId).subscribe(
      (response) => {
        // Check if the response body is valid JSON
        let message = '';
        try {
          message = JSON.parse(response.body).message;
        } catch (error) {
          console.error('Error parsing response body:', error);
          message = 'An error occurred while deleting the review.';
        }
        
        console.log('Review deletion response:', message);
        this.getAllReviews(); // Refresh the list of reviews after deletion
      },
      (error) => {
        console.error('Error occurred while deleting review:', error);
      }
    );
  }
  
  

  getReviewsByUserId() {
    const userId = localStorage.getItem('user');
    if (!userId) {
      console.error('User ID is not available in local storage');
      return; 
    }
  
    this.reviewService.getReviewsByUserId(userId).subscribe(
      (data: Review[]) => { 
        this.reviews = data;
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
