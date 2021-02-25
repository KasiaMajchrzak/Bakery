import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { NotifierModule, NotifierOptions } from "angular-notifier";
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { Connection } from './models/Connection';
import { DatabaseService } from './services/database.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule, MatDatepickerModule, MatDividerModule, MatExpansionModule, MatIconModule, MatInputModule, MatListModule, MatMenuModule, MatNativeDateModule, MatPaginatorModule, MatSidenavModule, MatSortModule, MatStepperModule, MatTableModule, MatToolbarModule } from '@angular/material';
import { DividerModule } from "primeng/divider";
import { PanelMenuModule } from 'primeng/panelmenu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BirthdayCakesComponent } from './layout/birthday-cakes/birthday-cakes.component';
import { BirthdayCakeComponent } from './layout/birthday-cakes/birthday-cake/birthday-cake.component';
import { MonoDessertsComponent } from './layout/mono-desserts/mono-desserts.component';
import { MonoDessertComponent } from './layout/mono-desserts/mono-dessert/mono-dessert.component';
import { CommonModule } from '@angular/common';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { Message } from 'primeng//api';
import { MessageService } from 'primeng/api';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DxAccordionModule } from 'devextreme-angular/ui/accordion';
import { DxTemplateModule } from 'devextreme-angular/core';

const routes: Routes = [
  { path : '', component: HomeComponent},
  { path : 'birthday-cakes/birthday-cakes', component: BirthdayCakesComponent },
  { path : 'birthday-cakes/birthday-cake', component: BirthdayCakeComponent },
  { path : 'birthday-cakes/birthday-cake/:id', component: BirthdayCakeComponent },
  { path : 'mono-desserts/mono-desserts', component: MonoDessertsComponent },
  { path : 'mono-desserts/mono-dessert', component: MonoDessertComponent },
  { path : 'mono-desserts/mono-dessert/:id', component: MonoDessertComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BirthdayCakeComponent,
    MonoDessertComponent,
    BirthdayCakesComponent,
    MonoDessertsComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    MatButtonModule,
    MatStepperModule,
    MatDividerModule,
    MatIconModule, 
    DividerModule,
    MatInputModule, 
    MatTableModule,
    MatPaginatorModule, 
    MatSortModule,
    MatCheckboxModule,
    MatSidenavModule,
    MatToolbarModule,
    MatMenuModule,
    MatListModule,
    PanelMenuModule,
    MatProgressSpinnerModule,
    MessageModule, 
    MessagesModule,
    NgbModule,
    MatExpansionModule,
    MatDatepickerModule,
    MatNativeDateModule,
    DxAccordionModule,
    DxTemplateModule
  ],
  providers: [Connection, DatabaseService, MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
