import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from "../../services/dataService";

@Component({
    selector: "login",
    templateUrl: "login.component.html",
    styleUrls: ["login.component.css"]
})
export class Login implements OnInit {
    public userNameError: boolean = false;
    public passwordError: boolean = false;

    constructor(private data: DataService, private router: Router) {
        
    }

    public errorMessage: string = "";

    public creds = {
        username: "",
        password: ""
    };

    ngOnInit(): void {
        this.data.logOut();
    }

    onLogin() {
        //if (!this.creds.username || this.creds.username.trim() === "") {
        //    this.userNameError = true;
        //    return;
        //}
        //else {
        //    this.userNameError = false;
        //}

        //if (!this.creds.password || this.creds.password.trim() === "") {
        //    this.passwordError = true
        //    return;
        //}
        //else {
        //    this.passwordError = false;
        //}

        this.data.login(this.creds)
            .subscribe(success => {
                if (success) {   
                  
                    this.router.navigate(["/news-feed"]);
                }
                else {
                    this.router.navigate(["/login"]);
                }
            }, err => this.errorMessage="You have failed me!")

        //if (this.data.isLoggedIn) {
        //    this.router.navigate(['/news-feed']);
        //}
    }
}