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
  baseProducts: any[] = [];
  selectedBaseProduct: any;

  menuItems: any[] = [
    { title: 'TORTY', path: 'birthday-cake'},
    { title: 'MONO-DESERY', path: 'mono-dessert'}
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: DatabaseService
  ) { }

  ngOnInit() {
    this.getBaseProducts();
    console.log("Home ngOnInit");
  }

  getBaseProducts() {
    this.service.SetRoute("baseproduct/getbaseproducts");
    this.service.GetObjList().subscribe(data => {
      this.baseProducts.push(data);
      console.log('getBaseProducts()', data);
    });
  }

  navigate(path: string){
    this.router.navigate([path]);
  }
}
