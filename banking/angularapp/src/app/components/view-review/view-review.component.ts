import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-view-review',
  templateUrl: './view-review.component.html',
  styleUrls: ['./view-review.component.css']
})
export class ViewReviewComponent implements OnInit {
  reviews: any[] = [];

  constructor( private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.getReviewsByUserId();
  }

  getReviewsByUserId() {
    this.reviewService.getReviewsByUserId().subscribe(
      (data: any) => {
        this.reviews = data;
        console.log(this.reviews)
      },
      (err) => {
        console.log(err);
      }
    );
  }

}