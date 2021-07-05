import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopPotionListComponent } from './shop-potion-list.component';

describe('ShopPotionListComponent', () => {
  let component: ShopPotionListComponent;
  let fixture: ComponentFixture<ShopPotionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShopPotionListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShopPotionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
