import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service';
import { Review } from 'src/app/models/review.model';
import { AuthService } from 'src/app/services/auth.service'; // Import AuthService here


@Component({
  selector: 'app-view-review',
  templateUrl: './view-review.component.html',
  styleUrls: ['./view-review.component.css']
})
export class ViewReviewComponent implements OnInit {
  reviews: Review[] = [];
  isAdmin: boolean = false;

  constructor(private reviewService: ReviewService, private authService: AuthService) { } // Inject AuthService here

  // ngOnInit(): void {
  //   this.getAllReviews();
  //   this.getReviewsByUserId();
  // }

  ngOnInit(): void {
    this.authService.getUserRole().subscribe(role => {
      if (role === 'Admin') {
        this.isAdmin = true;
        this.getAllReviews();
      } else {
        this.getReviewsByUserId();
      }
    });
  }

  getAllReviews() {
    this.reviewService.getAllReviews().subscribe(
      (data: Review[]) => { 
        this.reviews = data;
        console.log(this.reviews)
      },
      (err) => {
        console.log(err);
      }
    );
  }

  getReviewsByUserId() {
    const userId = localStorage.getItem('user');
    console.log(userId);
    if (!userId) {
      console.error('User ID is not available in local storage');
      return; 
    }
  
    this.reviewService.getReviewsByUserId(userId).subscribe(
      (data: Review[]) => { 
        this.reviews = data;
        console.log(this.reviews)
      },
      (err) => {
        console.log(err);
      }
    );
  }
  
}
