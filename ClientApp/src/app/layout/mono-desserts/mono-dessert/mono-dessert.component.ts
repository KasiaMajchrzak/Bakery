import { SelectionModel } from '@angular/cdk/collections';
import { DecimalPipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import {ActivatedRoute, Router} from "@angular/router";
import notify from 'devextreme/ui/notify';
import { MessageService } from 'primeng/api';
import { BaseProduct } from 'src/app/models/BaseProduct';
import { Cake } from 'src/app/models/Cake';
import { Cream } from 'src/app/models/Cream';
import { Order } from 'src/app/models/Order';
import { OrdersAdditionals } from 'src/app/models/OrdersAdditionals';
import { OrdersDecorations } from 'src/app/models/OrdersDecorations';
import { DatabaseService } from 'src/app/services/database.service';

@Component({
  selector: 'app-mono-dessert',
  templateUrl: './mono-dessert.component.html',
  styleUrls: ['./mono-dessert.component.scss']
})
export class MonoDessertComponent implements OnInit {
  urlIdParam: number;
  baseProduct: BaseProduct = new BaseProduct();
  order: Order = new Order();
  ordersAdditionals: OrdersAdditionals[] = [];
  ordersDecorations: OrdersDecorations[] = [];
  totalPrice: number = 0;
  invalidForm = true;
  today = new Date();
  cakes: Cake[] = [];
  dataSourceCakes: MatTableDataSource<Cake>;
  selectionCakes = new SelectionModel<Cake>(false, []);
  displayedColumnsCakes: string[] = ['select', 'name', 'type', 'flavour', 'description', 'price'];

  creams: Cream[] = [];
  dataSourceCreams: MatTableDataSource<Cream>;
  selectionCreams = new SelectionModel<Cream>(false, []);
  displayedColumnsCreams: string[] = ['select', 'name', 'type', 'flavour', 'description', 'price'];

  additionals: OrdersAdditionals[] = [];
  dataSourceAdditionals: MatTableDataSource<OrdersAdditionals>;
  selectionAdditionals = new SelectionModel<OrdersAdditionals>(true, []);
  displayedColumnsAdditionals: string[] = ['select', 'name', 'description', 'price'];

  decorations: OrdersDecorations[] = []
  dataSourceDecorations: MatTableDataSource<OrdersDecorations>;
  selectionDecorations = new SelectionModel<OrdersDecorations>(true, []);
  displayedColumnsDecorations: string[] = ['select', 'name', 'description', 'price'];

  @ViewChild('paginatorCakes', { read: MatPaginator, static: false }) paginatorCakes: MatPaginator;
  @ViewChild('tableCakes', { read: MatSort, static: false }) sorterCakes: MatSort;
  @ViewChild('paginatorCreams', { read: MatPaginator, static: false }) paginatorCreams: MatPaginator;
  @ViewChild('tableCreams', { read: MatSort, static: false }) sorterCreams: MatSort;
  @ViewChild('paginatorAdditionals', { read: MatPaginator, static: false }) paginatorAdditionals: MatPaginator;
  @ViewChild('tableAdditionals', { read: MatSort, static: false }) sorterAdditionals: MatSort;
  @ViewChild('paginatorDecorations', { read: MatPaginator, static: false }) paginatorDecorations: MatPaginator;
  @ViewChild('tableDecorations', { read: MatSort, static: false }) sorterDecorations: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: DatabaseService
  ) { }

  ngOnInit() {
    this.order.totalPrice = 0;
    this.getBaseProduct('Mono-deser');
    this.getCakes();
    this.getCreams();
    this.getAdditionals();
    this.getDecorations();
    this.detectIdParam();
  }

  getBaseProduct(name: string){
    this.service.SetRoute(`baseproduct/getbaseproductbyname?name=${name}`);
    this.service.GetObjList<any>().subscribe((data) => {
      this.baseProduct = data;
      this.order.baseProduct_Id = this.baseProduct.baseProduct_Id;
      this.totalPrice = this.baseProduct.price;
    });
  }

  getCakes(){
    this.service.SetRoute('cake/getcakes');
    this.service.GetObjList<any>().subscribe((data) => {
      this.cakes = data;
      this.cakes.forEach(x => x.price = x.price/4);

      this.dataSourceCakes = new MatTableDataSource<Cake>(this.cakes);
      this.setDataSourceConf(this.dataSourceCakes, this.sorterCakes, this.paginatorCakes);
    });
  }

  getCreams(){
    this.service.SetRoute('cream/getcreams');
    this.service.GetObjList<any>().subscribe((data) => {
      for(var i = 0; i < data.length; i++){
        this.creams.push(data[i]);
      }
      this.creams.forEach(x => x.price = x.price/4);
      this.dataSourceCreams = new MatTableDataSource<Cream>(this.creams);
      this.setDataSourceConf(this.dataSourceCreams, this.sorterCreams, this.paginatorCreams);
    });
  }

  getAdditionals(){
    this.service.SetRoute('additional/getadditionals');
    this.service.GetObjList<any>().subscribe((data) => {
      for(var i = 0; i< data.length; i++){
        this.additionals.push(new OrdersAdditionals(new Order(), data[i], 1));
      }
      this.additionals.forEach(x => {
        if(x.additional.price > 0) 
          x.additional.price = x.additional.price/2;
      });
      this.dataSourceAdditionals = new MatTableDataSource<OrdersAdditionals>(this.additionals);
      this.setDataSourceConf(this.dataSourceAdditionals, this.sorterAdditionals, this.paginatorAdditionals);
    });
  }

  getDecorations(){
    this.service.SetRoute('decoration/getdecorations');
    this.service.GetObjList<any>().subscribe((data) => {
      for(var i = 0; i< data.length; i++){
        this.decorations.push(new OrdersDecorations(new Order(), data[i], 1));
      }
      this.decorations.forEach(x => {
        if(x.decoration.price > 0) 
          x.decoration.price = x.decoration.price/5;
      });
      this.dataSourceDecorations = new MatTableDataSource<OrdersDecorations>(this.decorations);
      this.setDataSourceConf(this.dataSourceDecorations, this.sorterDecorations, this.paginatorDecorations);
    });
  }

  setDataSourceConf(dataSource: MatTableDataSource<any>, sorter: MatSort, paginator: MatPaginator){
    dataSource.sort = sorter;
    dataSource.paginator = paginator;
  }

  applyFilter(event, dataSource: MatTableDataSource<any>){
    const filterValue = (event.target as HTMLInputElement).value;
    dataSource.filter = filterValue.trim().toLowerCase();

    if (dataSource.paginator) {
      dataSource.paginator.firstPage();
    }
  }

  changed(event, selection: SelectionModel<any>, row: any){
    if(event){
      selection.toggle(row);
    } 
    if(this.selectionCreams.hasValue() && this.selectionCreams.hasValue())
      this.invalidForm = false;
    else 
      this.invalidForm = true;

    this.calculateTotalPrice();
  }

  calculateTotalPrice(){
    this.totalPrice = this.baseProduct.price;
    if(this.selectionCakes.selected.length > 0){
      this.totalPrice += this.selectionCakes.selected[0].price;
      this.order.cake = this.selectionCakes.selected[0];
      this.order.cake_Id = this.selectionCakes.selected[0].cake_Id;
    }
    if(this.selectionCreams.selected.length > 0){
      this.totalPrice += this.selectionCreams.selected[0].price;
      this.order.cream = this.selectionCreams.selected[0];
      this.order.cream_Id = this.selectionCreams.selected[0].cream_Id;
    }
    if(this.selectionAdditionals.selected.length > 0){
      for(var selected of this.selectionAdditionals.selected){
        this.totalPrice += (selected.additional.price * selected.quantity);
        this.order.totalPrice += (selected.additional.price * selected.quantity);
      }
      this.order.ordersAdditionals = this.selectionAdditionals.selected;
    }
    if(this.selectionDecorations.selected.length > 0){
      for(var selectedD of this.selectionDecorations.selected){
        this.totalPrice += (selectedD.decoration.price * selectedD.quantity);
        this.order.totalPrice += (selectedD.decoration.price * selectedD.quantity);
      }
      this.order.ordersDecorations = this.selectionDecorations.selected;
    }
    this.order.totalPrice = this.totalPrice * this.order.servings;
    this.order.discount = this.calculateDiscount(this.order.totalPrice);
  }

  addOrder(){
    const a = document.createElement('a');
    this.service.SetRoute('order/addorder');
    this.service.AddObjPDF<any>(this.order).subscribe((data) => {
      if(!data.result){
        const blob = new Blob([data], { type: 'application/pdf' });
        const url = window.URL.createObjectURL(blob);
        a.href = url;

        let filename = 'Zamówienie.pdf';
        a.download = filename;
        a.click();
        notify({ message: 'Dodano zamówienie!', position: 'top right', width: '450px' }, 'success', 2000);
      } else {
        notify({ message: 'Nie dodano zamówienia.', position: 'top right', width: '450px' }, 'error', 2000);
      } 
    });
  }

  getTemplate() {
    this.service.SetRoute(`order/gettemplatebyid?id=${this.urlIdParam}`);
    this.service.GetObjList<any>().subscribe((data) => {
      this.order.baseProduct = data.baseProduct;
      this.order.cake_Id = data.cake_Id;
      this.order.cream_Id = data.cream_Id;
      this.order.ordersAdditionals = data.ordersAdditionals;
      this.order.ordersDecorations = data.ordersDecorations;
      this.order.totalPrice = this.order.totalPrice;
      this.order.servings = data.servings;
      
      var selCake = this.dataSourceCakes.data.find(x => x.cake_Id === data.cake_Id);
      this.changed(true, this.selectionCakes, selCake);
      var selCream = this.dataSourceCreams.data.find(x => x.cream_Id === data.cream_Id);
      this.changed(true, this.selectionCreams, selCream);
      for(var orderAdd of this.order.ordersAdditionals) {
        var selAdd = this.dataSourceAdditionals.data.find(x => x.additional_Id === orderAdd.additional_Id);
        this.changed(true, this.selectionAdditionals, selAdd);
      }
      for(var orderDecor of this.order.ordersDecorations) {
        var selDec = this.dataSourceDecorations.data.find(x => x.decoration_Id === orderDecor.decoration_Id);
        this.changed(true, this.selectionDecorations, selDec);
      }
    });
  }

  detectIdParam() {
    this.route.params.subscribe((data) => {
      this.urlIdParam = data.id;
      if(this.urlIdParam) {
        this.getTemplate();
      }
    });
  }

  calculateDiscount(totalPrice: number): number {
    if (totalPrice > 150)
    {
      notify({ message:'Naliczono rabat w wysokości 20%!', position: 'top right', width: '450px' }, 'success', 2000);
      return totalPrice * 0.2; 
    }
    else if (totalPrice > 90)
    {
      notify({ message: 'Naliczono rabat w wysokości 10%!', position: 'top right', width: '450px' }, 'success', 2000);  
      return totalPrice * 0.1;
    }
    else
    {
        return 0;
    }
  }
}
