import { Component, OnInit } from '@angular/core';
import { Review } from 'src/app/models/review.model';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})
export class ReviewComponent implements OnInit {
  reviews: Review[] = [];
  newReview: Review = new Review();

  constructor(private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.loadReviews();
  }

  loadReviews(): void {
    this.reviewService.getAllReviews().subscribe(reviews => {
      this.reviews = reviews;
    });
  }

  addReview(): void {
    this.reviewService.addReview(this.newReview).subscribe(() => {
      this.newReview = new Review();
      this.loadReviews();
    });
  }
}
