import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatGridTileHeaderCssMatStyler, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import {ActivatedRoute, Router} from "@angular/router";
import { MessageService } from 'primeng/api';
import { Additional } from 'src/app/models/Additional';
import { BaseProduct } from 'src/app/models/BaseProduct';
import { Cake } from 'src/app/models/Cake';
import { Cream } from 'src/app/models/Cream';
import { Decoration } from 'src/app/models/Decoration';
import { Order } from 'src/app/models/Order';
import { OrdersAdditionals } from 'src/app/models/OrdersAdditionals';
import { OrdersDecorations } from 'src/app/models/OrdersDecorations';
import { DatabaseService } from 'src/app/services/database.service';

@Component({
  selector: 'app-birthday-cake',
  templateUrl: './birthday-cake.component.html',
  styleUrls: ['./birthday-cake.component.scss']
})
export class BirthdayCakeComponent implements OnInit {
  urlIdParam: number;
  order: Order = new Order();
  ordersAdditionals: OrdersAdditionals[] = [];
  ordersDecorations: OrdersDecorations[] = [];
  baseProduct: BaseProduct = new BaseProduct();
  totalPrice: number = 0;
  invalidForm = true;
  cakes: Cake[] = [];
  dataSourceCakes: MatTableDataSource<Cake> = new MatTableDataSource<Cake>();
  selectionCakes = new SelectionModel<Cake>(false, []);
  displayedColumnsCakes: string[] = ['select', 'name', 'type', 'flavour', 'description', 'price'];
  today = new Date();

  creams: Cream[] = [];
  dataSourceCreams: MatTableDataSource<Cream> = new MatTableDataSource<Cream>();
  selectionCreams = new SelectionModel<Cream>(false, []);
  displayedColumnsCreams: string[] = ['select', 'name', 'type', 'flavour', 'description', 'price'];

  additionals: OrdersAdditionals[] = [];
  dataSourceAdditionals: MatTableDataSource<OrdersAdditionals> = new MatTableDataSource<OrdersAdditionals>();
  selectionAdditionals = new SelectionModel<OrdersAdditionals>(true, []);
  displayedColumnsAdditionals: string[] = ['select', 'name', 'description', 'quantity', 'price'];

  decorations: OrdersDecorations[] = []
  dataSourceDecorations: MatTableDataSource<OrdersDecorations> = new MatTableDataSource<OrdersDecorations>();
  selectionDecorations = new SelectionModel<OrdersDecorations>(true, []);
  displayedColumnsDecorations: string[] = ['select', 'name', 'description', 'quantity', 'price'];
  

  @ViewChild('paginatorCakes', { static: false }) paginatorCakes: MatPaginator;
  @ViewChild('tableCakes', { read: MatSort, static: false }) sorterCakes: MatSort;
  @ViewChild('paginatorCreams', { static: false }) paginatorCreams: MatPaginator;
  @ViewChild('tableCreams', { read: MatSort, static: false }) sorterCreams: MatSort;
  @ViewChild('paginatorAdditionals', { static: false }) paginatorAdditionals: MatPaginator;
  @ViewChild('tableAdditionals', { read: MatSort, static: false }) sorterAdditionals: MatSort;
  @ViewChild('paginatorDecorations', { static: false }) paginatorDecorations: MatPaginator;
  @ViewChild('tableDecorations', { read: MatSort, static: false }) sorterDecorations: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: DatabaseService,
    private messageService: MessageService
  ) { }

  ngOnInit() {
    this.getBaseProduct('Tort');
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
      this.totalPrice = this.baseProduct.price;
      this.order.baseProduct_Id = this.baseProduct.baseProduct_Id;
      this.order.baseProduct = this.baseProduct;
    });
  }

  getCakes(){
    this.service.SetRoute('cake/getcakes');
    this.service.GetObjList<any>().subscribe((data) => {
      this.cakes = data;
      this.dataSourceCakes = new MatTableDataSource<Cake>(this.cakes);
      this.dataSourceCakes.sort = this.sorterCakes;
      this.dataSourceCakes.paginator = this.paginatorCakes;
    });
  }

  getCreams(){
    this.service.SetRoute('cream/getcreams');
    this.service.GetObjList<any>().subscribe((data) => {
      this.creams = data;
      this.dataSourceCreams = new MatTableDataSource<Cream>(this.creams);
      this.dataSourceCreams.sort = this.sorterCreams;
      this.dataSourceCreams.paginator = this.paginatorCreams;
    });
  }

  getAdditionals(){
    this.service.SetRoute('additional/getadditionals');
    this.service.GetObjList<any>().subscribe((data) => {
      for(var i = 0; i< data.length; i++){
        this.additionals.push(new OrdersAdditionals(new Order(), data[i], 1));
      }
      this.dataSourceAdditionals = new MatTableDataSource<OrdersAdditionals>(this.additionals);
      this.dataSourceAdditionals.sort = this.sorterAdditionals;
      this.dataSourceAdditionals.paginator = this.paginatorAdditionals;
    });
  }

  getDecorations(){
    this.service.SetRoute('decoration/getdecorations');
    this.service.GetObjList<any>().subscribe((data) => {
      for(var i = 0; i< data.length; i++){
        this.decorations.push(new OrdersDecorations(new Order(), data[i], 1));
      }
      this.dataSourceDecorations = new MatTableDataSource<OrdersDecorations>(this.decorations);
      this.dataSourceDecorations.sort = this.sorterDecorations;
      this.dataSourceDecorations.paginator = this.paginatorDecorations;
    });
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
    if(this.order.servings)
      this.order.totalPrice = this.totalPrice * this.order.servings;
    else 
      this.order.totalPrice = this.totalPrice;
    if(this.selectionAdditionals.selected.length > 0){
      for(var selected of this.selectionAdditionals.selected){
        this.totalPrice += (selected.additional.price * selected.quantity);
      }
      this.order.ordersAdditionals = this.selectionAdditionals.selected;
    }
    if(this.selectionDecorations.selected.length > 0){
      for(var selectedD of this.selectionDecorations.selected){
        this.totalPrice += (selectedD.decoration.price * selectedD.quantity);
      }
      this.order.ordersDecorations = this.selectionDecorations.selected;
    }

    this.order.totalPrice = this.totalPrice;
  }

  addOrder(){
    const a = document.createElement('a');
    console.log('addOrder()', this.order);
    this.service.SetRoute('order/addorder');
    this.service.AddObjPDF<any>(this.order).subscribe((data) => {
      if(!data.result){
        const blob = new Blob([data], { type: 'application/pdf' });
        const url = window.URL.createObjectURL(blob);
        a.href = url;

        let filename = 'Zamówienie.pdf';
        a.download = filename;
        a.click();
        this.messageService.add({ severity: 'success', summary: 'Dodano zamówienie!', detail: 'Wygenerowane zamówienie w formacie pdf zostało pobrane.'});
      } else {
        this.messageService.add({ severity: 'error', summary: 'Nie dodano zamówienia.'});
      } 
    });
  }

  getTemplate() {
    this.service.SetRoute(`order/gettemplatebyid?id=${this.urlIdParam}`);
    this.service.GetObjList<any>().subscribe((data) => {
      console.log('data', data);
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
}