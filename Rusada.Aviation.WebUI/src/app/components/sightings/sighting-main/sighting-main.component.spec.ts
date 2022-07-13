import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SightingMainComponent } from './sighting-main.component';

describe('SightingMainComponent', () => {
  let component: SightingMainComponent;
  let fixture: ComponentFixture<SightingMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SightingMainComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SightingMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
