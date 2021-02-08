import { Decoration } from "./Decoration";
import { Order } from "./Order";

export class OrdersDecorations {

    constructor(order?: Order, decoration?: Decoration, quantity?: number){
        if(order){
            this.order = order;
        }
        if(decoration){
            this.decoration_id = decoration.decoration_Id;
            this.decoration = decoration;
        }
        this.quantity = quantity ? quantity : 1;
    }

    id: number;
    order_id: number;
    decoration_id: number;
    quantity: number;
    order: Order;
    decoration: Decoration;
}