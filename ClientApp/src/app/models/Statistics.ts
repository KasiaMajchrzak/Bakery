import { Additional } from "./Additional";
import { Cake } from "./Cake";
import { Cream } from "./Cream";
import { Decoration } from "./Decoration";

export class Statistics<T> {
    object: T;
    numberOfOrders: number;
    objectName: string;
    orderedTogetherCakes: Statistics<Cake>[] = [];
    orderedTogetherCreams: Statistics<Cream>[] = [];
    orderedTogetherAdditionals: Statistics<Additional>[] = [];
    orderedTogetherDecorations: Statistics<Decoration>[] = [];
}