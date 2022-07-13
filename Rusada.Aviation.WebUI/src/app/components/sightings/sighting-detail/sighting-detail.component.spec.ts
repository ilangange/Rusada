import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SightingDetailComponent } from './sighting-detail.component';

describe('SightingDetailComponent', () => {
  let component: SightingDetailComponent;
  let fixture: ComponentFixture<SightingDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SightingDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SightingDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
