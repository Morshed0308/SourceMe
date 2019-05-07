import { Injectable } from "@angular/core"
import { Http, Response, Headers, RequestOptions } from "@angular/http";

@Injectable()
export class AuthTokenBearer {

    get getToken(): any {
        let headers = new Headers({ 'Authorization': 'Bearer ' + localStorage.getItem('token') });
        let options = new RequestOptions({ headers: headers });

        return options;

    }

} 