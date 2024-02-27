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
  constructor(private enquiryService: EnquiryService, private courseService: CourseService) { }

  ngOnInit(): void {
    this.getAllCourses();
  }

  getAllCourses(): void {
    this.courseService.getAllCourses().subscribe(courses => {
      this.courses = courses.map(course => course.courseName);
      console.log(courses);
    });
  }

  addEnquiry(): void {
    this.enquiryService.addEnquiry(this.enquiry).subscribe(() => {
      console.log('Enquiry added successfully');
      this.enquiry = new Enquiry(); // clear the form
    });
  }
}