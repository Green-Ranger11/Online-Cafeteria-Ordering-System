import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IMeal } from './models/meal';
import { IPagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Cafeteria Ordering System';
  meals: IMeal[];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/meals?pageSize=50').subscribe((response: IPagination) => {
      this.meals = response.data;
    }, error =>{
      console.log(error);
    });
  }
}
