import { Decoration } from "./Decoration";
import { Order } from "./Order";

export class OrdersDecorations {

    constructor(order?: Order, decoration?: Decoration, quantity?: number){
        if(order){
            this.order = order;
        }
        if(decoration){
            this.decoration_Id = decoration.decoration_Id;
            this.decoration = decoration;
        }
        this.quantity = quantity ? quantity : 1;
    }

    id: number;
    order_Id: number;
    decoration_Id: number;
    quantity: number;
    order: Order;
    decoration: Decoration;
    details?: string;
}