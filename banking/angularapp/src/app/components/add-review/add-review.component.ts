import { Component } from '@angular/core';
import { ReviewService } from '../../services/review.service';
import { NgForm } from '@angular/forms';
import { JwtService } from '../../services/jwt.service';
import { Router } from '@angular/router';
import { Review } from 'src/app/models/review.model';

@Component({
  selector: 'app-add-review',
  templateUrl: './add-review.component.html',
  styleUrls: ['./add-review.component.css']
})
export class AddReviewComponent {
  userRole: string;
  reviews: Review[] = [];
  newReview: Review = {
    ReviewId: 0,
    UserId: 0,
    Body: '',
    Rating: 0,
    DateCreated: new Date()
  };
  showReviewId: boolean = true; 

  constructor(private reviewService: ReviewService, private jwtService: JwtService, private router: Router) {
    this.userRole = this.jwtService.getUserRole();
    if (this.userRole === 'Customer') {
      this.showReviewId = true; // Show the ReviewId input field for admins
    } else {
      this.showReviewId = false; // Hide the ReviewId input field for other users
    }

    const userId = this.jwtService.getUserId(); // Assuming getUserId() returns the user ID from the token
    this.newReview.UserId = userId;
  }
 
  addReview(form: NgForm): void {
    if (form.valid) {
      console.log(this.newReview);
  
      this.reviewService.addReview(this.newReview).subscribe(
        (review) => {
          this.reviews.push(review);
          this.newReview = {
            ReviewId: 0,
            UserId: 0,
            Body: '',
            Rating: 0,
            DateCreated: new Date()
          };
          alert('Review added successfully!');
          this.router.navigate(['/view/review'])
        },
        (error) => {
          console.error('Error adding review:', error);
        }
      );
    }
  }
}
