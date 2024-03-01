// import { Component, OnInit } from '@angular/core';
// import { FixedDepositService } from '../../services/fixed-deposit.service';
// import { FixedDeposit } from 'src/app/models/fixedDeposit.model';
// import { UserRoles } from 'src/app/models/userRole.model';
// import { AuthService } from 'src/app/services/auth.service';

// @Component({
//   selector: 'app-view-fd',
//   templateUrl: './view-fd.component.html',
//   styleUrls: ['./view-fd.component.css']
// })
// export class ViewFdComponent implements OnInit {
//   fds: FixedDeposit[];
//   selectedFd: FixedDeposit;
//   userRole: string;
//   userId : number;

//   constructor(private fdService: FixedDepositService,private authService: AuthService) { }

//   ngOnInit(): void {
//     this.getAllFd();
//     this.userRole = localStorage.getItem('userRole'); // get the user's role from local storage
//     const token = localStorage.getItem('token');
//     if (token) {
//       const decodedToken = this.authService.decodeToken(token);
//       if (decodedToken) {
//         this.userRole = decodedToken.role;
//       }
//     }
//   }


//   getAllFd(): void {
//     this.fdService.getAllFd().subscribe(fds => {
//       this.fds = fds;
//       console.log(fds);
//     });
//   }

// editFd(fd: FixedDeposit): void {
//   console.log(this.editFd);
//   console.log('User Role:', this.userRole);

//   if (this.userRole !== 'Admin') {
//     console.error('Access denied. Only admins can edit FDs.');
//     return;
//   }
// }

//   deleteFd(fd: FixedDeposit): void {
//     if (this.userRole !== 'Admin') {
//       console.error('Access denied. Only admins can delete FDs.');
//       return;
//     }

//     this.fdService.deleteFdByAdmin(fd.fixedDepositId).subscribe(() => {
//       this.getAllFd(); // refresh the list after deleting
//     });
//   }

//   // updateFd(fd: FixedDeposit): void {
//   //   if (this.userRole !== 'Admin') {
//   //     console.error('Access denied. Only admins can update FDs.');
//   //     return;
//   //   }

//   //   this.fdService.updateFdByAdmin(fd.fixedDepositId, fd).subscribe(() => {
//   //     this.getAllFd(); // refresh the list after updating
//   //     this.selectedFd = null; // clear the selection
//   //   });
//   // }

//   cancelEdit(): void {
//     this.selectedFd = null; 
//   }
// }
import { Component, OnInit } from '@angular/core';
import { FixedDepositService } from '../../services/fixed-deposit.service';
import { FixedDeposit } from 'src/app/models/fixedDeposit.model';
import { UserRoles } from 'src/app/models/userRole.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-view-fd',
  templateUrl: './view-fd.component.html',
  styleUrls: ['./view-fd.component.css']
})
export class ViewFdComponent implements OnInit {
  fds: FixedDeposit[];
  selectedFd: FixedDeposit;
  userRole: string;
  editModeMap: { [key: number]: boolean } = {}; // Map to track the edit mode of each FD

  constructor(private fdService: FixedDepositService, private authService: AuthService) { }

  ngOnInit(): void {
    this.getAllFd();
    this.userRole = localStorage.getItem('userRole'); // get the user's role from local storage
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.authService.decodeToken(token);
      if (decodedToken) {
        this.userRole = decodedToken.role;
      }
    }
  }
  updateFd(fd: FixedDeposit): void {
    if (!fd.fixedDepositId) {
      console.error('Fixed deposit ID is undefined.');
          console.log('FD Object:', fd); // Log the fd object to inspect its structure

      return;
    }
    if (this.userRole !== 'Admin') {
      console.error('Access denied. Only admins can update FDs.');
      return;
    }
    
    const updatedData: FixedDeposit = { ...fd };
    // updatedData.fixedDepositId = fd.fixedDepositId; // Ensure that fixedDepositId is set correctly
  
    updatedData.amount = fd.amount;
    updatedData.tenureMonths = fd.tenureMonths;
    updatedData.interestRate = fd.interestRate;
  
    this.fdService.updateFdByAdmin(fd.fixedDepositId, updatedData).subscribe(
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
      fds.forEach(fd => this.editModeMap[fd.fixedDepositId] = false);
    });
  }

  editFd(fd: FixedDeposit): void {
    console.log('User Role:', this.userRole);

    if (this.userRole !== 'Admin') {
      console.error('Access denied. Only admins can edit FDs.');
      return;
    }
    this.editModeMap[fd.fixedDepositId] = !this.editModeMap[fd.fixedDepositId];

    this.selectedFd = fd;
  }
  deleteFd(fd: FixedDeposit): void {
    console.log('Deleting fixed deposit:', fd);
  
    if (this.fdService.deleteFdByAdmin) {
      this.fdService.deleteFdByAdmin(fd.FixedDepositId).subscribe(() => {
        this.getAllFd(); // Refresh the list after deleting
      });
    } else {
      console.error('deleteFdByAdmin method not found in FixedDepositService');
    }
  }
  

  cancelEdit(): void {
    this.editModeMap[this.selectedFd.fixedDepositId] = false;
    this.selectedFd = null; 
  }
}
