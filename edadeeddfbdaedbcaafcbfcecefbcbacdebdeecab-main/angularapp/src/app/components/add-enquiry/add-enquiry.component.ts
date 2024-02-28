import { Component, OnInit } from '@angular/core';
import { EnquiryService } from '../../services/enquiry.service';
import { Enquiry } from 'src/app/models/enquiry.model';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-add-enquiry',
  templateUrl: './add-enquiry.component.html',
  styleUrls: ['./add-enquiry.component.css']
})
export class AddEnquiryComponent implements OnInit {
  enquiry: Enquiry = new Enquiry();
  courses: any = [];
  enquiryDate: Date; // Define the property
  userId: string = 'sampleUserId'; // Initialize userId with a sample value
  status: string ='Pending';
  constructor(private enquiryService: EnquiryService, private courseService: CourseService) { }

  ngOnInit(): void {
    this.getAllCourses();
    this.enquiryDate = new Date(); // Set the current date
  }

  getAllCourses(): void {
    this.courseService.getAllCourses().subscribe(courses => {
      this.courses = courses.map(course => course.courseName);
      console.log(courses);
    });
  }

  addEnquiry(): void {
    this.enquiry.userId = this.userId;
this.enquiry.status='Pending';
    console.log('Enquiry Details:', this.enquiry); // Displaying the enquiry model values
    this.enquiryService.addEnquiry(this.enquiry).subscribe(() => {
      console.log('Enquiry added successfully');
      this.enquiry = new Enquiry(); // clear the form
      alert('Course added successfully!');
      this.router.navigate([''])


    });
  }
  getCurrentDate(): Date {
    return this.enquiryDate;
  }
}