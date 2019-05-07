import { OnInit, Component } from "@angular/core";
import { DataService } from "../../services/dataService";
import { Router } from "@angular/router";
import { FormGroup, FormBuilder, Validators, FormControl, RequiredValidator, EmailValidator } from '@angular/forms';
import { PassWordValidator } from "./passWordValidator.component";
import { last } from "@angular/router/src/utils/collection";


@Component({
    selector: 'signup',
    templateUrl: 'signup.component.html',
    styleUrls:[]


})

export class SignUp implements OnInit {

    signUpFormGroup: FormGroup;
    passwordFormGroup: FormGroup;



    firstName: FormControl;
    lastName: FormControl;
    userName: FormControl;
    email: FormControl;
    password: FormControl;
    confirmPassword: FormControl;

    public credens = {
        firstName: "",
        lastName: "",
        userName: "",
        email: "",
        password: "",
        confirmPassword: ""
    };

    
    public errorMsg: string = "";

    constructor(private data: DataService, private route: Router, private formBuild: FormBuilder) {
       
    }

    ngOnInit(): void {

        this.firstName = this.formBuild.control('', Validators.required);
        this.lastName = this.formBuild.control('', Validators.required);
        this.userName = this.formBuild.control('', Validators.minLength(4));
        this.email = this.formBuild.control('', Validators.email);
        this.password = this.formBuild.control('', Validators.minLength(8));
        this.confirmPassword = this.formBuild.control('', Validators.required);

        this.passwordFormGroup = this.formBuild.group({
            password: this.password,
            confirmPassword: this.confirmPassword

        }, {
                validator: PassWordValidator.validate.bind(this)
            });

        this.signUpFormGroup = this.formBuild.group({
            
            firstName: this.firstName,
            lastName: this.lastName,
            userName: this.userName,
            email: this.email,
            passwordFormGroup: this.passwordFormGroup

           
        });

        

        this.data.logOut();
    }
    onRegister() {
        this.credens.firstName = this.signUpFormGroup.controls.firstName.value;
        this.credens.lastName = this.signUpFormGroup.controls.lastName.value;
        this.credens.userName = this.signUpFormGroup.controls.userName.value;
        this.credens.email = this.signUpFormGroup.controls.email.value;
        this.credens.password = this.passwordFormGroup.controls.password.value;
        this.credens.confirmPassword = this.passwordFormGroup.controls.confirmPassword.value;

       
        this.data.signUp(this.credens)
            .subscribe(success => {
                if (success) {
                    this.route.navigate(['/login']);
                }
                else {
                    this.route.navigate(["/news-feed"]);
                }
            }, error=>this.errorMsg="Hello ! You have failed me!")


    }

    



}