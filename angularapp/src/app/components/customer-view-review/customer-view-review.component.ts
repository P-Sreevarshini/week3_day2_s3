import { Component, OnInit } from '@angular/core';
import { ResortService } from 'src/app/services/resort.service';

@Component({
  selector: 'app-customer-view-review',
  templateUrl: './customer-view-review.component.html',
  styleUrls: ['./customer-view-review.component.css']
})
export class CustomerViewReviewComponent implements OnInit {
  reviews: any[] = [];

  constructor( private resortService: ResortService) { }

  ngOnInit(): void {
    this.getReviewsByUserId();
  }

  getReviewsByUserId() {
    this.resortService.getReviewsByUserId().subscribe(
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
