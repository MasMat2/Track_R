import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '@models/seguridad/login-request';
import { LoginResponse } from '@models/seguridad/login-response';
import { Observable, throwError, from as observableFrom, switchMap } from 'rxjs';
import { catchError } from 'rxjs/operators';
import * as forge from 'node-forge';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private dataUrl = 'login/';
  private publicKey: forge.pki.rsa.PublicKey;
  private publicKeyReady: Promise<void>;

  constructor(public http: HttpClient) { 
    this.publicKeyReady = this.setServerPublicKey();
  }

  
  private setServerPublicKey(): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      this.http.get(this.dataUrl + 'obtenerLlavePublica', { responseType: 'text' }).subscribe({
        next: (publicKeyBase64: string) => {
          const publicKeyBytes = forge.util.decode64(publicKeyBase64);
          const publicKeyAsn1 = forge.asn1.fromDer(publicKeyBytes);
          this.publicKey = forge.pki.publicKeyFromAsn1(publicKeyAsn1);
          resolve();
        },
        error: (error) => {
          reject(error);
        }
      }
      );
    });
  }

  public isPublicKeyReady(): Promise<boolean> {
    return this.publicKeyReady.then(
      () => !!this.publicKey,
      () => false
    );
  }

  public authenticate(loginRequest: LoginRequest): Observable<LoginResponse> {
    return observableFrom(this.setServerPublicKey()).pipe(
      switchMap(() => {
        if (!this.publicKey) {
          return throwError(() => new Error('Public key is not set.'));
        }
  
        // Copy loginRequest to encrypt password, and keep original loginRequest object
        var encryptedLoginRequest = new LoginRequest();
        Object.assign(encryptedLoginRequest, loginRequest);
        
        // Encrypt the password
        const encrypted = this.publicKey.encrypt(loginRequest.contrasena, "RSA-OAEP");
  
        // Convert to Base64 string
        encryptedLoginRequest.contrasena = forge.util.encode64(encrypted);
  
        var esMobile = false;
  
        return this.http.post<LoginResponse>(this.dataUrl + `authenticate/${esMobile}`, encryptedLoginRequest);
      }),
      catchError(error => {
        console.error('Authentication failed:', error);
        return throwError(() => error);
      })
    );
  }
}
