import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service';
import { Review } from 'src/app/models/review.model';

@Component({
  selector: 'app-view-review',
  templateUrl: './view-review.component.html',
  styleUrls: ['./view-review.component.css']
})
export class ViewReviewComponent implements OnInit {
  reviews: Review[] = [];

  constructor(private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.getAllReviews();
    this.getReviewsByUserId();
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
    const userId = localStorage.getItem('userId');
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
