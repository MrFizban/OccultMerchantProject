import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IAmErrorComponent } from './iam-error.component';

describe('IAmErrorComponent', () => {
  let component: IAmErrorComponent;
  let fixture: ComponentFixture<IAmErrorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IAmErrorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IAmErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
