import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuyDrawTicketPageComponent } from './buy-draw-ticket-page.component';

describe('BuyDrawTicketPageComponent', () => {
  let component: BuyDrawTicketPageComponent;
  let fixture: ComponentFixture<BuyDrawTicketPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BuyDrawTicketPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BuyDrawTicketPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
