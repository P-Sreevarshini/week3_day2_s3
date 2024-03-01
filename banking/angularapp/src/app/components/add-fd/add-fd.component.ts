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
export class AddFdComponent {
  userRole: string;
  fds: FixedDeposit[] = [];
  newFd: FixedDeposit = {
    fixedDepositId: 0,
    amount: 0,
    tenureMonths: 0,
    interestRate: 0,
  };
  showFdId: boolean = true; 

  constructor(private fdService: FixedDepositService, private jwtService: JwtService, private router: Router) {
    this.userRole = this.jwtService.getUserRole();
    if (this.userRole === 'Admin') {
      this.showFdId = true; 
    } else {
      this.showFdId = false; 
    }
  }
 
  addFd(form: NgForm): void {
    if (form.valid) {
      console.log(this.newFd);
  
      this.fdService.saveFdByAdmin(this.newFd).subscribe(
        (fd) => {
          this.fds.push(fd);
          this.newFd = { fixedDepositId: 0, amount: 0, tenureMonths: 0, interestRate: 0 };
          alert('Fixed deposit added successfully!');
          this.router.navigate(['/view/FD'])
        },
        (error) => {
          console.error('Error adding fixed deposit:', error);
        }
      );
    }
  }
}
