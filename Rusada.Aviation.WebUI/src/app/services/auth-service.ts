import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { User } from "../_models/user";

const jwtHelper = new JwtHelperService();

@Injectable()
export class AuthService {
    private isLoggedIn: BehaviorSubject<any> = new BehaviorSubject(false);

    constructor(private http: HttpClient) {
        //this.isLoggedIn = this.checkLogin();
    }

    login(username: string, password: string) {
        return this.http.post<User>(environment.base_api + 'v1/auth/login', { username, password });
    }

    checkLogin(): Observable<boolean> {
        var token = localStorage.getItem('token');
        if (token !== null && token !== ''){
            this.isLoggedIn.next(true);
        }
        else{
            this.isLoggedIn.next(false);
        }
        return this.isLoggedIn;
    }

    saveToken(token: any) {
        if (token !== null) {
            localStorage.setItem('token', token);
            this.isLoggedIn.next(true);
        }
        else {
            localStorage.removeItem('token');
            this.isLoggedIn.next(false);
        }
    }

    isAuthenticated(): boolean {
        const token = localStorage.getItem('token');
        return !jwtHelper.isTokenExpired(token?.toString());
      }

}