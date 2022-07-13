import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SightingListComponent } from './sighting-list.component';

describe('SightingListComponent', () => {
  let component: SightingListComponent;
  let fixture: ComponentFixture<SightingListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SightingListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SightingListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
