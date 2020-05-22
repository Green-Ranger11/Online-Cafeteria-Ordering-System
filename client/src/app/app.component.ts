import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IMeal } from './shared/models/meal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Cafeteria Ordering System';

  constructor() {}

  ngOnInit(): void {
  }
}
