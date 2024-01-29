import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { CreateJitsiMeetComponent } from './create-jitsi-meet.component';

describe('CreateJitsiMeetComponent', () => {
  let component: CreateJitsiMeetComponent;
  let fixture: ComponentFixture<CreateJitsiMeetComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateJitsiMeetComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(CreateJitsiMeetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
