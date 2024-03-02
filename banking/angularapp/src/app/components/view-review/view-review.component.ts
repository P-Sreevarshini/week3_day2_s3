import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-customer-view-review',
  templateUrl: './customer-view-review.component.html',
  styleUrls: ['./customer-view-review.component.css']
})
export class CustomerViewReviewComponent implements OnInit {
  reviews: any[] = [];

  constructor( private resortService: ReviewService) { }

  ngOnInit(): void {
    this.getReviewsByUserId();
  }

  getReviewsByUserId() {
    getReviewsByUserId(userId: number): Observable<any> {
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
