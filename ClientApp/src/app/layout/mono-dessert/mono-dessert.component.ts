import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { BaseProduct } from 'src/app/models/BaseProduct';
import { DatabaseService } from 'src/app/services/database.service';

@Component({
  selector: 'app-mono-dessert',
  templateUrl: './mono-dessert.component.html',
  styleUrls: ['./mono-dessert.component.scss']
})
export class MonoDessertComponent implements OnInit {
  baseProduct: BaseProduct;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: DatabaseService
  ) { }

  ngOnInit() {
    console.log("MonoDessertComponent works");
    this.getBaseProduct('Mono-deser');
  }

  getBaseProduct(name: string){
    this.service.SetRoute(`baseproduct/getbaseproductbyname?name=${name}`);
    this.service.GetObjList<any>().subscribe((data) => {
      this.baseProduct = data;
    });
  }
}
