import { Component, OnInit } from '@angular/core';
import { EnquiryService } from '../../services/enquiry.service';
import { Enquiry } from 'src/app/models/enquiry.model';

@Component({
  selector: 'app-view-enquiry',
  templateUrl: './view-enquiry.component.html',
  styleUrls: ['./view-enquiry.component.css']
})
export class ViewEnquiryComponent implements OnInit {
  enquiries: Enquiry[];
  userRole: string;

  constructor(private enquiryService: EnquiryService) { }

  ngOnInit(): void {
    this.userRole = localStorage.getItem('userRole'); // get the user's role from local storage
    this.getEnquiries();
  }

  getEnquiries(): void {
    this.enquiryService.getAllEnquiries().subscribe(enquiries => {
      this.enquiries = enquiries;
    });
  }

  deleteEnquiry(enquiry: Enquiry): void {
    if (this.userRole !== 'STUDENT') {
      console.error('Access denied. Only students can delete enquiries.');
      return;
    }

    this.enquiryService.deleteEnquiry(enquiry).subscribe(() => {
      this.getEnquiries(); // refresh the list after deleting
    });
  }
}