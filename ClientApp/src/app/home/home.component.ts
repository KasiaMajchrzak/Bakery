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
  selectDesertType = false;
  isCreator = false;
  isTemplate = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: DatabaseService
  ) { }

  ngOnInit() {
    this.selectDesertType = false;
    this.isCreator = false;
    this.isTemplate = false;
  }

  return() {
    this.selectDesertType = false;
    this.isCreator = false;
    this.isTemplate = false;
  }

  navigate(path: string){
    this.router.navigate([path]);
  }

  navigateToBirthdayCake() {
    if (this.isCreator) {
      this.navigate('birthday-cakes/birthday-cake');
    } else {
      this.navigate('birthday-cakes/birthday-cakes');
    }
  }

  navigateToMonoDessert() {
    if (this.isCreator) {
      this.navigate('mono-desserts/mono-dessert');
    } else {
      this.navigate('mono-desserts/mono-desserts');
    }
  }

  selectCreator() {
    this.selectDesertType = true;
    this.isCreator = true;
    this.isTemplate = false;
  }

  selectTemplates() {
    this.selectDesertType = true;
    this.isTemplate = true;
    this.isCreator = false;
  }
}
