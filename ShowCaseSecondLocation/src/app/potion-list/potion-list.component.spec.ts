import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PotionListComponent } from './potion-list.component';

describe('PotionListComponent', () => {
  let component: PotionListComponent;
  let fixture: ComponentFixture<PotionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PotionListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PotionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
