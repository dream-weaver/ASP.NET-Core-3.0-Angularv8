import { Component } from "@angular/core";
import { DataService } from "../shared/dataservice";
import { Router } from '@angular/router';

@Component({
    selector: "checkout",
    templateUrl: "checkout.component.html",
    styleUrls: ['checkout.component.css']
})

export class CheckoutComponent {
    constructor(private data: DataService, private router: Router) { }
    title = "Checkout";
    errorMessage: string = "";

    onCheckout() {
        this.data.checkout()
        .subscribe(success => {
            if (success) {
                this.router.navigate([""]);
            }
        }, err => this.errorMessage = "Failed to do order!")

    }
}