import { Additional } from "./Additional";
import { Order } from "./Order";

export class OrdersAdditionals {

    constructor(order?: Order, additional?: Additional, quantity?: number){
        if(order){
            this.order = order;
        }
        if(additional){
            this.additional_Id = additional.additional_Id;
            this.additional = additional;
        }
        this.quantity = quantity ? quantity : 1;
    }

    id: number;
    order_Id: number;
    additional_Id: number;
    quantity: number;
    order: Order;
    additional: Additional;
    details?: string;
}