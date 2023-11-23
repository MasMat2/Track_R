import { Component } from '@angular/core';
import { ChatDTO } from '@dtos/chats/chat-dto';
import { ChatHubServiceService } from '@services/chat-hub-service.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  protected chats$: Observable<ChatDTO[]>;

  constructor(
    private ChatHubServiceService:ChatHubServiceService
  ) { }

  ngOnInit(): void {
    this.chats$ = this.ChatHubServiceService.chat$
    console.log(this.chats$)
  }
}
