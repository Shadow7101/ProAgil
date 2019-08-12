import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    public authService: AuthService,
    public router: Router) {
  }

  ngOnInit() {
  }

  Logado(): boolean {
    return this.authService.Logado();
  }

  Logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }

}
