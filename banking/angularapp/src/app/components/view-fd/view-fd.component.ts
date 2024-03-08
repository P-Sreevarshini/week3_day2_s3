import { Component, OnInit } from '@angular/core';
import { FixedDepositService } from '../../services/fixed-deposit.service';
import { FixedDeposit } from 'src/app/models/fixedDeposit.model';
import { UserRoles } from 'src/app/models/userRole.model';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { FDAccount } from 'src/app/models/fixedDepositAccount';


@Component({
  selector: 'app-view-fd',
  templateUrl: './view-fd.component.html',
  styleUrls: ['./view-fd.component.css']
})
export class ViewFdComponent implements OnInit {
  fds: FixedDeposit[];
  fda: FDAccount[];
  selectedFd: FixedDeposit;
  userRole: string;
  editModeMap: { [key: number]: boolean } = {}; // Map to track the edit mode of each FD

  constructor(private fdService: FixedDepositService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.getAllFd();
    this.userRole = localStorage.getItem('userRole');
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.authService.decodeToken(token);
      if (decodedToken) {
        this.userRole = decodedToken.role;
      }
    }
  }
  updateFd(fd: FixedDeposit): void {
    if (!fd.FixedDepositId) {
      console.error('Fixed deposit ID is undefined.');
          console.log('FD Object:', fd); // Log the fd object to inspect its structure

      return;
    }
    if (this.userRole !== 'Admin') {
      console.error('Access denied. Only admins can update FDs.');
      return;
    }
    
    const updatedData: FixedDeposit = { ...fd };  
    updatedData.amount = fd.amount;
    updatedData.tenureMonths = fd.tenureMonths;
    updatedData.interestRate = fd.interestRate;
  
    this.fdService.updateFdByAdmin(fd.FixedDepositId, updatedData).subscribe(
      () => {
        console.log('Fixed deposit updated successfully.');
        this.getAllFd();
      },
      (error) => {
        console.error('Error updating fixed deposit:', error);
      }
    );
  }
  
  toggleEditMode(fd: FixedDeposit): void {
    this.selectedFd = this.selectedFd === fd ? null : fd;
  }
  

  getAllFd(): void {
    this.fdService.getAllFd().subscribe(fds => {
      this.fds = fds;
      fds.forEach(fd => this.editModeMap[fd.FixedDepositId] = false);
    });
  }

  editFd(fd: FixedDeposit): void {
    // console.log('User Role:', this.userRole);

    if (this.userRole !== 'Admin') {
      console.error('Access denied. Only admins can edit FDs.');
      return;
    }
    this.editModeMap[fd.FixedDepositId] = !this.editModeMap[fd.FixedDepositId];

    this.selectedFd = fd;
  }
  deleteFd(fd: FixedDeposit): void {
    console.log('Deleting fixed deposit:', fd);

    if (this.fdService.deleteFdByAdmin) {

      this.fdService.deleteFdByAdmin(fd.FixedDepositId).subscribe(() => {
        this.getAllFd();
      });
    } else {
      console.error('deleteFdByAdmin method not found in FixedDepositService');
    }
  }

  cancelEdit(): void {
    this.editModeMap[this.selectedFd.FixedDepositId] = false;
    this.selectedFd = null; 
  }

  // createAccount(fd: FixedDeposit): void {
  //   this.router.navigate(['/add/FDaccount'], { queryParams: { ...fd } });
  //   console.log(fd);
  // }

  createAccount(fd: FixedDeposit): void {
    const userId = localStorage.getItem('user');
    const fixedDepositId = fd.FixedDepositId;
    const status = 'pending';
  
    if (!userId) {
      console.error('User ID not found.');
      return;
    }
  
    const newAccountData = {
      UserId: userId,
      Status: status,
      FixedDepositId: fixedDepositId
    };
  console.log(newAccountData);
    this.fdService.createFdAccount(newAccountData).subscribe(
      () => {
        console.log('New FD account created successfully.');
        this.router.navigate(['/view/FDaccount']); // Navigate to another page upon success
      },
      (error) => {
        console.error('Error creating FD account:', error); // Log the error
      }
    );
  }
  
  
}
