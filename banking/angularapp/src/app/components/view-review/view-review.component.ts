import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service';
import { Review } from 'src/app/models/review.model';
import { AuthService } from 'src/app/services/auth.service';

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
  deleteReview(userId: Review): void {
    console.log(Review);
    if (this.userRole !== 'Customer') {
      console.error('Access denied. Only customers can delete reviews.');
      return;
    }

    this.reviewService.deleteReview(userId).subscribe(() => {
      this.getAllReviews(); // Refresh the list of reviews after deletion
    });
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
