import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { BookingService } from 'src/app/services/booking.service';
import { Booking } from 'src/app/models/booking.model';
import { ResortService } from 'src/app/services/resort.service';

@Component({
  selector: 'app-add-booking',
  templateUrl: './add-booking.component.html',
  styleUrls: ['./add-booking.component.css'],
})
export class AddBookingComponent implements OnInit {
  resort: any = [];
  resorts: any = [];
  booking: any = [];
  addBookingForm: FormGroup;
  errorMessage = '';
  showSuccessPopup = false;
  confirmPayment = false;
  paymentSuccess = false; // New variable to control the success message display

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private resortService: ResortService,
    private bookingService: BookingService,
    private router: Router
  ) {
    this.addBookingForm = this.fb.group({
      // resortId: [''],
      resortName: [''],
      resortLocation: [''],
      totalPrice: [''],
      capacity: [''],
      address: ['', Validators.required],
      noOfPersons: ['', Validators.required],
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
    }, { validators: this.dateRangeValidator });
  }

  ngOnInit() {
    const resortId = this.route.snapshot.paramMap.get('id');
    this.getResortById(resortId);
    this.getAllBookings();
  }

  dateRangeValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const fromDate = control.get('fromDate')?.value;
    const toDate = control.get('toDate')?.value;

    if (fromDate && toDate) {
      const fromDateObj = new Date(fromDate);
      const toDateObj = new Date(toDate);

      if (fromDateObj > toDateObj) {
        return { 'dateRangeError': true };
      }
    }

    return null;
  }
  getAllBookings() {
    this.bookingService.getAllBookings().subscribe((response: any) => {
      this.resorts = response;
      console.log("this.route.snapshot.paramMap.get('id')", this.route.snapshot.paramMap.get('id'))
      const resortBookings = this.resorts.filter((booking) => booking.resortId == this.route.snapshot.paramMap.get('id'));
      this.booking = resortBookings;
      //       const totalNoOfPersons = resortBookings.reduce((sum, booking) => sum + booking.noOfPersons, 0);
      // console.log("totall->>>>>>>>>>>",totalNoOfPersons)

      console.log("All booking info", resortBookings, this.resorts);
    });
  }


  getResortById(resortId) {
    this.resortService.getResortById(resortId).subscribe((response: any) => {
      console.log(response);
      this.resort = response;

      // Use patchValue to pre-fill specific form fields
      this.addBookingForm.patchValue({
        resortId: response.resortId,
        resortName: response.resortName,
        resortLocation: response.resortLocation,
        totalPrice: response.price,
        capacity: response.capacity,
      });
      console.log(this.addBookingForm.value)
    });
  }
  selectedDate: string; // Make sure to use the correct data type for your date

  getTodayDate(): string {
    const today = new Date();
    const month = (today.getMonth() + 1).toString().padStart(2, '0');
    const day = today.getDate().toString().padStart(2, '0');
    const formattedDate = `${today.getFullYear()}-${month}-${day}`;
    return formattedDate;
  }


  onSubmit(): void {
    //   if (this.confirmPayment) {
    //     // The "Cancel" button was clicked, do not submit the form
    //     return;
    // }


    console.log(this.addBookingForm)
    if (this.addBookingForm.valid) {
      const newBooking = this.addBookingForm.value;





      const requestObj: Booking = {
        userId: Number(localStorage.getItem('userId')),
        resortId: this.resort.resortId,
        address: newBooking.address,
        noOfPersons: newBooking.noOfPersons,
        fromDate: newBooking.fromDate,
        toDate: newBooking.toDate,
        totalPrice: newBooking.totalPrice,
        status: 'PENDING',
      };
      console.log(requestObj)


      const overlappingBookings = this.booking.filter((booking) => {
        const fromDate = new Date(newBooking.fromDate);
        const toDate = new Date(newBooking.toDate);
        const bookingFromDate = new Date(booking.fromDate);
        const bookingToDate = new Date(booking.toDate);

        return (
          (fromDate >= bookingFromDate && fromDate <= bookingToDate) ||
          (toDate >= bookingFromDate && toDate <= bookingToDate) ||
          (fromDate <= bookingFromDate && toDate >= bookingToDate)
        );
      });

      console.log("overlappingBookings-----------array-----", overlappingBookings);

      let capacity = parseInt(localStorage.getItem("capacity"), 10);
      if (newBooking.noOfPersons > capacity) {
        this.errorMessage = "Number of persons count exceeds resort capacity"
      } else if (overlappingBookings.length) {
        this.errorMessage = ""
        let totalNoOfPersonsInDateRange = overlappingBookings.reduce((sum, booking) => sum + booking.noOfPersons, 0);
        console.log("totalNoOfPersonsInDateRange", totalNoOfPersonsInDateRange, "------", newBooking.noOfPersons, "------", capacity);
        // console.log("overlappingBookings",overlappingBookings,"condition",((totalNoOfPersonsInDateRange + newBooking.noOfPersons) - 1) >localStorage.getItem("capacity"));

        console.log(" newBooking.noOfPersons--------->", newBooking.noOfPersons);


        // Compare the total number of persons in the date range with the capacity

        console.log("totalNoOfPersonsInDateRange + newBooking.noOfPersons>capacity", totalNoOfPersonsInDateRange + newBooking.noOfPersons > capacity);

        if (totalNoOfPersonsInDateRange + newBooking.noOfPersons > capacity) {
          this.errorMessage = 'Capacity exceeded for selected dates. Please choose different dates or reduce the number of persons.';
          console.log("Capacity exceeded for selected dates. Please choose different dates or reduce the number of persons.");
        }
        else {
          this.errorMessage = ""

          this.confirmPayment = true;

        }



        // this.errorMessage = 'Please select different dates. No booking available on these days';

        //  console.log("Please select different dates. No booking available on these days")
      }
      else {
        this.errorMessage = ""
        this.confirmPayment = true;
        // this.bookingService.addBooking(requestObj).subscribe(
        //   (response) => {
        //     console.log('Booking added successfully', response);
        //     this.addBookingForm.reset(); // Reset the form
        //   },
        //   (error) => {
        //     console.error('Error adding booking', error);
        //   }
        // );
        console.log("Came in else")
      }

    } else {
      this.errorMessage = 'All fields are required';
    }
  }

  navigateToDashboard() {
    this.router.navigate(['/customer/view/bookings']);
  }

  makePayment() {
    // Logic to handle payment confirmation
    // For demonstration purposes, setting showSuccessPopup to true

    const newBooking = this.addBookingForm.value;
    const requestObj: Booking = {
      userId: Number(localStorage.getItem('userId')),
      resortId: this.resort.resortId,
      address: newBooking.address,
      noOfPersons: newBooking.noOfPersons,
      fromDate: newBooking.fromDate,
      toDate: newBooking.toDate,
      totalPrice: newBooking.totalPrice,
      status: 'PENDING',
    };
    this.bookingService.addBooking(requestObj).subscribe(
      (response) => {
        console.log('Booking added successfully', response);
        this.addBookingForm.reset(); // Reset the form
      },
      (error) => {
        console.error('Error adding booking', error);
      }
    );

    this.paymentSuccess = true;
    this.confirmPayment = false; // Close the confirmation dialog
  }

  cancelPayment() {
    // Logic to handle cancellation
    this.confirmPayment = false; // Close the confirmation dialog
  }
}
