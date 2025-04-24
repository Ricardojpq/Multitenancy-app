import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrawResultPageComponent } from './draw-result-page.component';

describe('DrawResultPageComponent', () => {
  let component: DrawResultPageComponent;
  let fixture: ComponentFixture<DrawResultPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DrawResultPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DrawResultPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
