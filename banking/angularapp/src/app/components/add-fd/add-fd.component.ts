import { Component } from '@angular/core';
import { FixedDepositService } from '../../services/fixed-deposit.service';
import { NgForm } from '@angular/forms';
import { JwtService } from '../../services/jwt.service';
import { Router } from '@angular/router';
import { FixedDeposit } from 'src/app/models/fixedDeposit.model';

@Component({
  selector: 'app-add-fd',
  templateUrl: './add-fd.component.html',
  styleUrls: ['./add-fd.component.css']
})
export class AddFdComponent  {
  userRole: string;
  fds: FixedDeposit[] = [];
  newFd: FixedDeposit = {
    fixedDepositId: 0,
    amount: 0,
    tenureMonths: 0,
    interestRate: 0,
    startDate: new Date()
  };
  showFdId: boolean = true; // Flag to show/hide the fixedDepositId input field

  constructor(private fdService: FixedDepositService, private jwtService: JwtService, private router: Router) {
    this.userRole = this.jwtService.getUserRole();
    if (this.userRole === 'Admin') {
      this.showFdId = true; // Show the fixedDepositId input field for admins
    } 
    // else {
    //   this.showFdId = false; // Hide the fixedDepositId input field for other users
    // }
  }
 
  addFd(form: NgForm): void {
    if (form.valid) {
      console.log(this.newFd);
  
      this.fdService.saveFdByAdmin(this.newFd).subscribe(
        (fd) => {
          this.fds.push(fd);
          this.newFd = { fixedDepositId: 0, amount: 0, tenureMonths: 0, interestRate: 0, startDate: new Date() };
          
          // Display alert message
          alert('Fixed deposit added successfully!');
          this.router.navigate(['view/FD'])
        },
        (error) => {
          console.error('Error adding fixed deposit:', error); // Log error
        }
      );
    }
  }
}
