import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewSightingComponent } from './new-sighting.component';

describe('NewSightingComponent', () => {
  let component: NewSightingComponent;
  let fixture: ComponentFixture<NewSightingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewSightingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewSightingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
