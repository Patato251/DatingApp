import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-members-card',
  templateUrl: './members-card.component.html',
  styleUrls: ['./members-card.component.css']
})
export class MembersCardComponent implements OnInit {
  // Passing input of member list user as this is a child component to members
  @Input() user: User;
  
  constructor() { }

  ngOnInit() {
  }

}
