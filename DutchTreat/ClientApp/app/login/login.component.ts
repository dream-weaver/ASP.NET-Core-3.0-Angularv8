import { Component } from "@angular/core";
import { DataService } from "../shared/dataservice";
import { Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: 'login.component.html',
    styleUrls: ['login.component.css']
})

export class LoginComponent {
    constructor(private data: DataService, private router: Router) { }

    errorMessage: string = "";

    public creds = {
        username: "",
        password: ""
    };

    onLogin() {       
    this.data.login(this.creds)
        .subscribe(success => {
            if (success) {
                if (this.data.order.items.length) {
                    this.router.navigate(["checkout"]);
                } else {
                    this.router.navigate([""]);
                }
            }
        }, err => this.errorMessage = "Failed to login!")
    }
}