import { __decorate } from "tslib";
import { Component } from "@angular/core";
let LoginComponent = class LoginComponent {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        this.errorMessage = "";
        this.creds = {
            username: "",
            password: ""
        };
    }
    onLogin() {
        this.data.login(this.creds)
            .subscribe(success => {
            if (success) {
                if (this.data.order.items.length) {
                    this.router.navigate(["checkout"]);
                }
                else {
                    this.router.navigate([""]);
                }
            }
        }, err => this.errorMessage = "Failed to login!");
    }
};
LoginComponent = __decorate([
    Component({
        selector: 'login',
        templateUrl: 'login.component.html',
        styleUrls: ['login.component.css']
    })
], LoginComponent);
export { LoginComponent };
//# sourceMappingURL=login.component.js.map