import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OmniListComponent } from './omni-list.component';

describe('OmniListComponent', () => {
  let component: OmniListComponent;
  let fixture: ComponentFixture<OmniListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OmniListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OmniListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
