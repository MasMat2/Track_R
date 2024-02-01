import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnswerMeetComponent } from './answer-meet.component';

describe('AnswerMeetComponent', () => {
  let component: AnswerMeetComponent;
  let fixture: ComponentFixture<AnswerMeetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AnswerMeetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnswerMeetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
