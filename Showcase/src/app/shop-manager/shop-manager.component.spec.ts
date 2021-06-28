import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopManagerComponent } from './shop-manager.component';

describe('ShopManagerComponent', () => {
  let component: ShopManagerComponent;
  let fixture: ComponentFixture<ShopManagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShopManagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShopManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
