import { Injectable } from "@angular/core";
import { MatPaginatorIntl } from "@angular/material";

@Injectable()
export class MatPaginatorTranslateService extends MatPaginatorIntl {
    itemsPerPageLabel = 'Liczba elementów na stronie:';
    nextPageLabel = 'Następna strona';
    previousPageLabel = 'Poprzednia strona';
    firstPageLabel = "Pierwsza strona";
    lastPageLabel = "Ostatnia strona";
}