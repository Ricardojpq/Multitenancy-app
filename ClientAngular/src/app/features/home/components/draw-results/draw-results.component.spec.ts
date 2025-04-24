import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrawResultsComponent } from './draw-results.component';

describe('DrawResultsComponent', () => {
  let component: DrawResultsComponent;
  let fixture: ComponentFixture<DrawResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DrawResultsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DrawResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
