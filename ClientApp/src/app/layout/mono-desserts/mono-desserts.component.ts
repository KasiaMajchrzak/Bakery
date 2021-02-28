import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { BaseProduct } from "src/app/models/BaseProduct";
import { Order } from "src/app/models/Order";
import { DatabaseService } from "src/app/services/database.service";

@Component({
    selector: 'app-mono-desserts',
    templateUrl: './mono-desserts.component.html',
    styleUrls: ['./mono-desserts.component.scss']
})
export class MonoDessertsComponent implements OnInit {
    
    baseProduct: BaseProduct;
    templates: Order[] = [];

    constructor(
      private router: Router,
      private service: DatabaseService
    ) {}

    ngOnInit(){
      this.getBaseProduct('Mono-deser');
    }

    getBaseProduct(name: string) {
      this.service.SetRoute(`baseproduct/getbaseproductbyname?name=${name}`);
      this.service.GetObjList<any>().subscribe(data => {
        this.baseProduct = data;
        this.getTemplates(this.baseProduct.baseProduct_Id);
      });
    }

    getTemplates(baseProductId: number){
      this.service.SetRoute(`order/getordertemplates?baseProductId=${baseProductId}`);
      this.service.GetObjList<any>().subscribe(data => {
        this.templates = data;
      })
    }

    personalize(orderId: number) {
      this.router.navigate(['/mono-desserts/mono-dessert', orderId]);
    }
}