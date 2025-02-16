import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44391/api/auth'; // Replace with .NET API URL

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, { username, password }).pipe(
      tap(response => {
        sessionStorage.setItem('token', response.token); // Store JWT token
      })
    );
  }

  logout() {
    sessionStorage.removeItem('token');
  }

  isAuthenticated(): boolean {
    return sessionStorage.getItem('token') !== null;
  }
}
