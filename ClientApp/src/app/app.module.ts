import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { NotifierModule, NotifierOptions } from "angular-notifier";
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { MonoDessertComponent } from './layout/mono-dessert/mono-dessert.component';
import { BirthdayCakeComponent } from './layout/birthday-cake/birthday-cake.component';
import { Connection } from './models/Connection';
import { DatabaseService } from './services/database.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule, MatDividerModule, MatIconModule, MatInputModule, MatPaginatorModule, MatSortModule, MatStepperIntl, MatStepperModule, MatTableModule } from '@angular/material';
import { DividerModule } from "primeng/divider";
import { NotificationService } from './services/notification.service';


const routes: Routes = [
  { path : '', component: HomeComponent},
  { path : 'birthday-cake', component: BirthdayCakeComponent },
  { path : 'mono-dessert', component: MonoDessertComponent }
];

const customNotifierOptions: NotifierOptions = {
  position: {
		horizontal: {
			position: 'right',
			distance: 12
		},
		vertical: {
			position: 'top',
			distance: 12,
			gap: 10
		}
	},
  theme: 'material',
  behaviour: {
    autoHide: 5000,
    onClick: 'hide',
    onMouseover: 'pauseAutoHide',
    showDismissButton: true,
    stacking: 4
  },
  animations: {
    enabled: true,
    show: {
      preset: 'slide',
      speed: 300,
      easing: 'ease'
    },
    hide: {
      preset: 'fade',
      speed: 300,
      easing: 'ease',
      offset: 50
    },
    shift: {
      speed: 300,
      easing: 'ease'
    },
    overlap: 150
  }
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BirthdayCakeComponent,
    MonoDessertComponent
  ],
  imports: [
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
    NotifierModule.withConfig(customNotifierOptions)
  ],
  providers: [Connection, DatabaseService, NotificationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
