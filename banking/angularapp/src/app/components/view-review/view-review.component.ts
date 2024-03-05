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
  isAdmin: boolean = true;
 isCustomer: boolean = true;
 userRole: string;



  constructor(private reviewService: ReviewService, private authService: AuthService) { } // Inject AuthService here

  // ngOnInit(): void {
  //   this.authService.getUserRole().subscribe(UserRole => {
  //     if (UserRole === 'Admin') {
  //       console.log(UserRole);
  //       this.isAdmin = true;
  //       this.getAllReviews();
  //     } 
  //     else if (UserRole === 'Customer') {
  //       console.log(UserRole);
  //       this.isCustomer = true;

  //       this.getReviewsByUserId();
  //     }
  //   });
  // }
  ngOnInit(): void {
    this.userRole = localStorage.getItem('userRole'); // get the user's role from local storage
        this.getReviewsByUserId();
        this.getAllReviews();

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
