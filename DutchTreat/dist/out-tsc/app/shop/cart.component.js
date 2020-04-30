import { __decorate } from "tslib";
import { Component } from "@angular/core";
let CartComponent = class CartComponent {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        this.title = "Shopping Cart";
    }
    onCheckout() {
        if (this.data.loginRequired) {
            // Force Login
            this.router.navigate(["login"]);
        }
        else {
            // Go to Checkout
            this.router.navigate(["checkout"]);
        }
    }
};
CartComponent = __decorate([
    Component({
        selector: "the-cart",
        templateUrl: "cart.component.html",
        styleUrls: []
    })
], CartComponent);
export { CartComponent };
//# sourceMappingURL=cart.component.js.map