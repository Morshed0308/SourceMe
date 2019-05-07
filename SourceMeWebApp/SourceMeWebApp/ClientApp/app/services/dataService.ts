import { Http, Response, Headers,RequestOptions } from "@angular/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { FeedItem } from "../models/feedItem";
import { Category } from "../models/category";
import { AuthTokenBearer } from "./tokenService";


@Injectable()
export class DataService {

    constructor(private http: Http, private atokenbearer: AuthTokenBearer) {
        
    }

    public token: string = "";
    public tokenExpiration: Date;
    

    public saveItem: any = {
        token:this.token,
        expiration:this.tokenExpiration
    };

  

    public feedItems: FeedItem[] = [];
    public categories: Category[] = [];
    public subCategories: Category[] = [];



    public showNavBar: boolean;

    get isLoggedIn(): boolean {

        if (localStorage.getItem('token') != null) {
            if (localStorage.getItem('token').length == 0) {
                this.logOut();
                return false;
            }

            else {
                this.logIn();
                return true;
            }


        }

        
        
    }


    public logIn() {
        this.showNavBar = true;
       
    }

    public logOut() {
        this.showNavBar = false;
        localStorage.removeItem('token');
    }


    public login(creds) {
        return this.http.post("/account/createtoken", creds)
            .map(response => {
                let tokenInfo = response.json();
                this.token = tokenInfo.token;
                console.log(this.token);
                this.tokenExpiration = tokenInfo.expiration;
                if (this.token) {
                    localStorage.setItem('token', tokenInfo.token);
                }
                
               
         
                return true;
            });

    }

    public signUp(creds) {
        return this.http.post("/account/SignUpUserasync", creds)
            .map(response => {
                return true;

            });

    }






    
    async loadCategories() {

        this.categories = JSON.parse(localStorage.getItem('categories'));
        if (!this.categories || this.categories.length <= 0) {
            this.categories = [];
            //let headers = new Headers({ 'Authorization': 'Bearer ' + localStorage.getItem('token') });
            //let options = new RequestOptions({ headers: headers });

            await this.http.get(`api/categories`, this.atokenbearer.getToken)
                .toPromise()
                .then(data => {
                    let categories = data.json();
                    //default select
                    categories[0].subCategories[0].isChecked = true;
                    this.categories = categories;
                    localStorage.setItem('categories', JSON.stringify(this.categories));
                });
        }

        this.subCategories = JSON.parse(localStorage.getItem('subCategories'));
        if (!this.subCategories || this.subCategories.length <= 0) {
            let subCatgs = [];
            this.categories.forEach((catg, index) => {  
                catg.subCategories.forEach((subCatg) => {
                    subCatgs.push(subCatg);
                });
            });
            this.subCategories = subCatgs;
            localStorage.setItem('subCategories', JSON.stringify(this.subCategories));
        }

        return this.categories;
    }

    public loadNewsFeed(categoryId: number, channelId: number): Observable<FeedItem[]> {
        //let headers = new Headers({ 'Authorization': 'Bearer ' + localStorage.getItem('token') });
        //let options = new RequestOptions({ headers: headers });

        return this.http.get(`/api/news-feed/${categoryId}/${channelId}`, this.atokenbearer.getToken)
            .map((result: Response) => this.feedItems = result.json());

        
    }


}