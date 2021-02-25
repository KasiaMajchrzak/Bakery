import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { MessageService } from "primeng/api";
import { BaseProduct } from "src/app/models/BaseProduct";
import { Order } from "src/app/models/Order";
import { DatabaseService } from "src/app/services/database.service";

@Component({
    selector: 'app-birthday-cakes',
    templateUrl: './birthday-cakes.component.html',
    styleUrls: ['./birthday-cakes.component.scss']
  })
export class BirthdayCakesComponent implements OnInit {
    baseProduct: BaseProduct;
    templates: Order[] = [];

    constructor(
      private router: Router,
      private service: DatabaseService,
      private messageService: MessageService
    ) {}

    ngOnInit(){
      this.getBaseProduct('Tort');
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
        console.log('this.templates', data);
        this.templates = data;
      })
    }

    personalize(orderId: number) {
      this.router.navigate(['/birthday-cakes/birthday-cake', orderId]);
    }
}