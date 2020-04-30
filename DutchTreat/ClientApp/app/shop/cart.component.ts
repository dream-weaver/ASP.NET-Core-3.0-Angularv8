import { Component } from "@angular/core";
import { DataService } from "../shared/dataservice";
import { Router } from '@angular/router';

@Component({
    selector: "the-cart",
    templateUrl: "cart.component.html",
    styleUrls:[]
})

export class CartComponent {
    constructor(private data: DataService, private router: Router) { }
    title = "Shopping Cart";
    onCheckout() {
        if (this.data.loginRequired) {
            // Force Login
            this.router.navigate(["login"]);
        } else {
            // Go to Checkout
            this.router.navigate(["checkout"]);
        }
    }
}