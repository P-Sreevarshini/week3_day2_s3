import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardComponent } from './dashboard.component';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('DashboardComponent_should_be_create', () => {
    expect(component).toBeTruthy();
  });
  fit('DashboardComponent_should_set_isAdmin_to_true_if_user_is_admin', () => {
    // Mock AuthService
    const authServiceMock = jasmine.createSpyObj('AuthService', ['isAdmin']);
    authServiceMock.isAdmin.and.returnValue(true);
  
    // Create DashboardComponent instance
    const component = new DashboardComponent(authServiceMock);
  
    // Call ngOnInit()
    component.ngOnInit();
  
    // Check if isAdmin is true
    expect(component.isAdmin).toBe(true);
  });
  
  fit('DashboardComponent_should_set_isAdmin_to_false_if_user_is_not_admin', () => {
    // Mock AuthService
    const authServiceMock = jasmine.createSpyObj('AuthService', ['isAdmin']);
    authServiceMock.isAdmin.and.returnValue(false);
  
    // Create DashboardComponent instance
    const component = new DashboardComponent(authServiceMock);
  
    // Call ngOnInit()
    component.ngOnInit();
  
    // Check if isAdmin is false
    expect(component.isAdmin).toBe(false);
  });
  
});
