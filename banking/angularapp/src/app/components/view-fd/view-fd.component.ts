import { Component, OnInit } from '@angular/core';
import { FixedDepositService } from '../../services/fixed-deposit.service';
import { FixedDeposit } from 'src/app/models/fixedDeposit.model';

@Component({
  selector: 'app-view-fd',
  templateUrl: './view-fd.component.html',
  styleUrls: ['./view-fd.component.css']
})
export class ViewFdComponent implements OnInit {
  fds: FixedDeposit[];
  selectedFd: FixedDeposit;
  userRole: string;

  constructor(private fdService: FixedDepositService) { }

  ngOnInit(): void {
    this.getAllFd();
    this.userRole = localStorage.getItem('userRole'); // get the user's role from local storage
  }

  getAllFd(): void {
    this.fdService.getAllFdByAdmin().subscribe(
      (fds: FixedDeposit[]) => {
        this.fds = fds;
        console.log('Fetched fixed deposits:', this.fds);
      },
      (error) => {
        console.error('Error fetching fixed deposits:', error);
      }
    );
  }

  editFd(fd: FixedDeposit): void {
    if (this.userRole != 'Admin') {
      console.error('Access denied. Only admins can edit FDs.');
      return;
    }

    this.selectedFd = fd;
  }

  deleteFd(fd: FixedDeposit): void {
    if (this.userRole !== 'ADMIN') {
      console.error('Access denied. Only admins can delete FDs.');
      return;
    }

    this.fdService.deleteFdByAdmin(fd.fixedDepositId).subscribe(() => {
      this.getAllFd(); // refresh the list after deleting
    });
  }

  updateFd(fd: FixedDeposit): void {
    if (this.userRole !== 'ADMIN') {
      console.error('Access denied. Only admins can update FDs.');
      return;
    }

    this.fdService.updateFdByAdmin(fd.fixedDepositId, fd).subscribe(() => {
      this.getAllFd(); // refresh the list after updating
      this.selectedFd = null; // clear the selection
    });
  }

  cancelEdit(): void {
    this.selectedFd = null; // clear the selection
  }
}
