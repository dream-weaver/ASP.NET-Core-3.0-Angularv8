import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductListComponent } from './shop/productList.component';
import { CartComponent } from './shop/cart.component';
import { ShopComponent } from './shop/shop.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { LoginComponent } from './login/login.component';

import { DataService } from './shared/dataservice';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

let routes = [
    { path: "", component: ShopComponent },
    { path: "checkout", component: CheckoutComponent },
    { path: "login", component: LoginComponent}
]

@NgModule({
    declarations: [
        AppComponent,
        ProductListComponent,
        CartComponent,
        ShopComponent,
        CheckoutComponent,
        LoginComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot(routes, {
            useHash: true,
            enableTracing: false // for debugging of the routes
        })
    ],
    providers: [DataService],
    bootstrap: [AppComponent]
})
export class AppModule { }
