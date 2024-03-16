// add-course.component.ts
import { Component } from '@angular/core';
import { Course } from 'src/app/models/course.model';
import { CourseService } from '../../services/course.service';
import { NgForm } from '@angular/forms';
import { JwtService } from '../../services/jwt.service';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css']
})
export class AddCourseComponent  {
  userRole: string;
  courses: Course[] = [];
  newCourse: Course = {
    courseID: 0,
    courseName: '',
    description: '',
    duration: '',
    cost: 0
  };

  constructor(private courseService: CourseService, private jwtService: JwtService) {
    this.userRole = this.jwtService.getUserRole();
  }
 
  addCourse(form: NgForm): void {
    if (form.valid) {
      console.log(this.newCourse);
      
      this.courseService.saveCourseByAdmin(this.newCourse).subscribe(course => {
        this.courses.push(course);
        this.newCourse = { courseID: 0, courseName: '', description: '', duration: '', cost: 0 };
      });
    }
  }
}