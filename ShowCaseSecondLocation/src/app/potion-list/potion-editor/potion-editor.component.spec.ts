import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PotionEditorComponent } from './potion-editor.component';

describe('PotionEditorComponent', () => {
  let component: PotionEditorComponent;
  let fixture: ComponentFixture<PotionEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PotionEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PotionEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
