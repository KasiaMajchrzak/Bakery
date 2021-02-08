import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import {ActivatedRoute, Router} from "@angular/router";
import { Additional } from 'src/app/models/Additional';
import { BaseProduct } from 'src/app/models/BaseProduct';
import { Cake } from 'src/app/models/Cake';
import { Cream } from 'src/app/models/Cream';
import { Decoration } from 'src/app/models/Decoration';
import { Order } from 'src/app/models/Order';
import { OrdersAdditionals } from 'src/app/models/OrdersAdditionals';
import { OrdersDecorations } from 'src/app/models/OrdersDecorations';
import { DatabaseService } from 'src/app/services/database.service';
import { NotificationService } from 'src/app/services/notification.service'

@Component({
  selector: 'app-birthday-cake',
  templateUrl: './birthday-cake.component.html',
  styleUrls: ['./birthday-cake.component.scss']
})
export class BirthdayCakeComponent implements OnInit {
  order: Order = new Order();
  ordersAdditionals: OrdersAdditionals[] = [];
  ordersDecorations: OrdersDecorations[] = [];
  baseProduct: BaseProduct;
  totalPrice: number = 0;
  invalidForm = true;
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
  displayedColumnsAdditionals: string[] = ['select', 'name', 'description', 'quantity', 'price'];

  decorations: OrdersDecorations[] = []
  dataSourceDecorations: MatTableDataSource<OrdersDecorations>;
  selectionDecorations = new SelectionModel<OrdersDecorations>(true, []);
  displayedColumnsDecorations: string[] = ['select', 'name', 'description', 'quantity', 'price'];
  

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
    private service: DatabaseService,
    private notification: NotificationService
  ) { }

  ngOnInit() {
    console.log("BirthdayComponent on init");
    this.getBaseProduct('Tort');
    this.getCakes();
    this.getCreams();
    this.getAdditionals();
    this.getDecorations();
    this.paginatorCakes._intl.itemsPerPageLabel = "Ilość elementów na stronie: ";
    this.paginatorCreams._intl.itemsPerPageLabel = "Ilość elementów na stronie: ";
    this.paginatorAdditionals._intl.itemsPerPageLabel = "Ilość elementów na stronie: ";
    this.paginatorDecorations._intl.itemsPerPageLabel = "Ilość elementów na stronie: ";
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
      this.setDataSourceConf(this.dataSourceCakes, this.sorterCakes, this.paginatorCakes);
    });
  }

  getCreams(){
    this.service.SetRoute('cream/getcreams');
    this.service.GetObjList<any>().subscribe((data) => {
      this.creams = data;
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
      }
      this.order.ordersAdditionals = this.selectionAdditionals.selected;
    }
    if(this.selectionDecorations.selected.length > 0){
      for(var selectedD of this.selectionDecorations.selected){
        this.totalPrice += (selectedD.decoration.price * selectedD.quantity);
      }
      this.order.ordersDecorations = this.selectionDecorations.selected;
    }
    this.generateOrder();
  }

  generateOrder(){
   
  }

  addOrder(){
    console.log('addOrder()', this.order);
    this.service.SetRoute('order/addorder');
    this.service.AddObj<any>(this.order).subscribe((data) => {
      if(data.result){
        this.notification.showNotification('success', 'Dodano zamówienie!');
      } else {
        this.notification.showNotification('danger', 'Nie dodano zamówienia.')
      }
    });
  }
}