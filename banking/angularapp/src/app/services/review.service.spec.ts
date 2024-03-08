import { TestBed, async } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ReviewService } from './review.service';
import { Review } from '../models/review.model';

describe('ReviewService', () => {
  let service: ReviewService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ReviewService],
    });

    service = TestBed.inject(ReviewService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('Frontend_ReviewService should get all reviews', () => {
    const mockToken = 'mockToken';
    const mockReviews: Review[] = [
      { ReviewId: 1, UserId: 1, Body: 'Great experience', Rating: 4, DateCreated: new Date()},
      { ReviewId: 2, UserId: 2, Body: 'Excellent service', Rating: 5, DateCreated: new Date()}
    ];

    spyOn(localStorage, 'getItem').and.returnValue(mockToken);

    service.getAllReviews().subscribe((reviews) => {
      expect(reviews.length).toBe(2);
      expect(reviews).toEqual(mockReviews);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/api/Review`);
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBe('Bearer ' + mockToken);

    req.flush(mockReviews);
  });

  fit('Frontend_ReviewService should get reviews by user ID', () => {
    const mockToken = 'mockToken';
    const userId = 1;
    const mockReviews: Review[] = [
      { ReviewId: 1, UserId: userId, Body: 'Great experience', Rating: 4, DateCreated: new Date()},
      { ReviewId: 2, UserId: userId, Body: 'Excellent service', Rating: 5, DateCreated: new Date() }
    ];

    spyOn(localStorage, 'getItem').and.returnValue(mockToken);

    service.getReviewsByUserId(userId.toString()).subscribe((reviews) => {
      expect(reviews.length).toBe(2);
      expect(reviews).toEqual(mockReviews);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/api/Review/${userId}`);
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBe('Bearer ' + mockToken);

    req.flush(mockReviews);
  });
});
