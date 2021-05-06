import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { DxPieChartComponent } from "devextreme-angular";
import { Additional } from "src/app/models/Additional";
import { BaseProduct } from "src/app/models/BaseProduct";
import { Cake } from "src/app/models/Cake";
import { Cream } from "src/app/models/Cream";
import { Decoration } from "src/app/models/Decoration";
import { Statistics } from "src/app/models/Statistics";
import { DatabaseService } from "src/app/services/database.service";

@Component({
    selector: 'app-statistics',
    templateUrl: './statistics.component.html',
    styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent implements OnInit {
    cakeStatistics: Statistics<Cake>[] = [];
    creamStatistics: Statistics<Cream>[] = [];
    decorationStatistics: Statistics<Decoration>[] = [];
    additionalStatistics: Statistics<Additional>[] = [];
    baseProductStatistics: Statistics<BaseProduct>[] = [];
    selectedType: any;
    selectedElement = 'Ciasta';
    sale: any[] = [];
    selectedStatistic: Statistics<any> = new Statistics<any>();

    chartType: any[] = [
        { value: 0, name: 'Ciasta' },
        { value: 1, name: 'Kremy' },
        { value: 2, name: 'Dodatki' },
        { value: 3, name: 'Dekoracje' }
    ];

    @ViewChild(DxPieChartComponent, { static: false }) pieChart: DxPieChartComponent;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: DatabaseService
    ) {}

    ngOnInit() {
        this.selectedType = 0;
        this.getBaseProductsStatistics();
        this.getCakeStatistics();
        this.getCreamStatistics();
        this.getAdditionalStatistics();
        this.geDecorationStatistics();
    }

    click(e) {
        e.target.select();
        this.selectedStatistic = e.target.data; 
    }

    customizeTooltip(arg: any) {
        return {
            text: "<strong>" + arg.argumentText + ": " + arg.valueText + "</strong><br/>" + "Kliknij, żeby zobaczyć najczęściej zamawiane razem elementy."
        };
    }

    customizeTooltipChart(arg: any) {
        return {
            text: "<strong>" + arg.argumentText + "</strong>"
        };
    }

    changeChartType() {
        this.selectedStatistic = new Statistics<any>();
        switch (this.selectedType) {
            case 0:
                this.sale = this.cakeStatistics;
                this.selectedElement = 'Ciasta';
                break;
            case 1:
                this.sale = this.creamStatistics;
                this.selectedElement = 'Kremy';
                break;
            case 2:
                this.sale = this.additionalStatistics;
                this.selectedElement = 'Dodatki';
                break;
            case 3:
                this.sale = this.decorationStatistics;
                this.selectedElement = 'Dekoracje';
                break;
            default:
                this.sale = this.cakeStatistics;
                this.selectedElement = 'Ciasta';
                break;
        }
    }

    getBaseProductsStatistics() {
        this.service.SetRoute('statistics/getbaseproductstatistics');
        this.service.GetObjList<any>().subscribe((data) => {
            this.baseProductStatistics = data;
        });
    }

    getCakeStatistics() {
        this.service.SetRoute('statistics/getcakestatistics');
        this.service.GetObjList<any>().subscribe((data) => {
            this.cakeStatistics = data;
            this.sale = data;
            console.log('this.sale', data);
        });
    }

    getCreamStatistics() {
        this.service.SetRoute('statistics/getcreamstatistics');
        this.service.GetObjList<any>().subscribe((data) => {
            this.creamStatistics = data;
        });
    }

    geDecorationStatistics() {
        this.service.SetRoute('statistics/getdecorationstatistics');
        this.service.GetObjList<any>().subscribe((data) => {
            this.decorationStatistics = data;
        });
    }

    getAdditionalStatistics() {
        this.service.SetRoute('statistics/getadditionalstatistics');
        this.service.GetObjList<any>().subscribe((data) => {
            this.additionalStatistics = data;
        });
    }
}