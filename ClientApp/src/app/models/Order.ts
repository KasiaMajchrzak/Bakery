import {BaseProduct} from "./BaseProduct";
import {Cream} from "./Cream";
import {Cake} from "./Cake";
import { OrdersAdditionals } from "./OrdersAdditionals";
import { OrdersDecorations } from "./OrdersDecorations";

export class Order {
  order_Id?: number;
  baseProduct_Id: number;
  cream_Id: number;
  cake_Id: number;
  servings: number;
  orderedOn?: Date;
  totalPrice?: number;
  isTemplate: boolean;
  completionDate: Date;
  baseProduct?: BaseProduct;
  cream?: Cream;
  cake?: Cake;
  ordersAdditionals?: OrdersAdditionals[] = [];
  ordersDecorations?: OrdersDecorations[] = [];
  templateName?: string;
  discount: number;
}