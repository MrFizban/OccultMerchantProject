import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWeaponFormComponent } from './add-weapon-form.component';

describe('AddWeaponFormComponent', () => {
  let component: AddWeaponFormComponent;
  let fixture: ComponentFixture<AddWeaponFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddWeaponFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddWeaponFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
