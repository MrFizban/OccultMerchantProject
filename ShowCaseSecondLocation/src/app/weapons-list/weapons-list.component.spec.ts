import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeaponsListComponent } from './weapons-list.component';

describe('WeaponsListComponent', () => {
  let component: WeaponsListComponent;
  let fixture: ComponentFixture<WeaponsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeaponsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WeaponsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
