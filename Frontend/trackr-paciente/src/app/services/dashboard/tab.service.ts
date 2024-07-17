import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TabService {
  private tabChangeSource = new Subject<string>();
  tabChange$ = this.tabChangeSource.asObservable();

  changeTab(tabId: string) {
    this.tabChangeSource.next(tabId);
  }
}
