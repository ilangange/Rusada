import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Rusada.Aviation.Web';
  isLoggedIn: boolean = false;

  constructor(
    private router: Router, private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.authService.checkLogin().subscribe(res => {
      if (res != null) {
        this.isLoggedIn = res;
      }
    });
  }

  login() {
    this.router.navigate(['/login']);
  }

  logout() {
    this.authService.saveToken(null);
    this.router.navigate(['/']);
  }
}
