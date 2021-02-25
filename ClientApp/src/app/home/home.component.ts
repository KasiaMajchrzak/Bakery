import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { BaseProduct } from '../models/BaseProduct';
import { DatabaseService } from "../services/database.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: DatabaseService
  ) { }

  ngOnInit() {
    console.log("Home ngOnInit");
  }

  navigate(path: string){
    this.router.navigate([path]);
  }
}
