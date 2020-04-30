import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ProductListComponent = class ProductListComponent {
    constructor(data) {
        this.data = data;
        this.title = "Product List";
        this.products = [];
    }
    ngOnInit() {
        this.data.loadProducts()
            .subscribe(success => {
            if (success) {
                this.products = this.data.products;
            }
        });
    }
    addProduct(product) {
        this.data.addOrder(product);
    }
};
ProductListComponent = __decorate([
    Component({
        selector: "product-list",
        templateUrl: "./productList.component.html",
        styleUrls: ["./productList.component.css"]
    })
], ProductListComponent);
export { ProductListComponent };
//# sourceMappingURL=productList.component.js.map