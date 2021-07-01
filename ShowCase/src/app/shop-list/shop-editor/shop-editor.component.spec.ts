import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopEditorComponent } from './shop-editor.component';

describe('ShopEditorComponent', () => {
  let component: ShopEditorComponent;
  let fixture: ComponentFixture<ShopEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShopEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShopEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
