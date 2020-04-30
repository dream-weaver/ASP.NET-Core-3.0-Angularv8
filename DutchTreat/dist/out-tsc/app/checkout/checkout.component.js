import { __decorate } from "tslib";
import { Component } from "@angular/core";
let CheckoutComponent = class CheckoutComponent {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        this.title = "Checkout";
        this.errorMessage = "";
    }
    onCheckout() {
        this.data.checkout()
            .subscribe(success => {
            if (success) {
                this.router.navigate([""]);
            }
        }, err => this.errorMessage = "Failed to do order!");
    }
};
CheckoutComponent = __decorate([
    Component({
        selector: "checkout",
        templateUrl: "checkout.component.html",
        styleUrls: ['checkout.component.css']
    })
], CheckoutComponent);
export { CheckoutComponent };
//# sourceMappingURL=checkout.component.js.map