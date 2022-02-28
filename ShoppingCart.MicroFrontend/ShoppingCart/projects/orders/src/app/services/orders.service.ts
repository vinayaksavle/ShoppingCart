import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Orders } from "../models/orders";
const baseUrl = "https://localhost:44386/api/ShoppingCart";
@Injectable({
    providedIn: 'root'
})
export class OrdersService {
    constructor(private http: HttpClient) {
    }

    getAll(): Observable<Orders[]> {
        return this.http.get<Orders[]>(baseUrl);
    }
    get(id: any): Observable<Orders> {
        return this.http.get(`${baseUrl}/${id}`);
    }
    create(data: any): Observable<any> {
        return this.http.post(baseUrl, data);
    }
    update(id: any, data: any): Observable<any> {
        return this.http.put(`${baseUrl}/${id}`, data);
    }
    delete(id: any): Observable<any> {
        return this.http.delete(`${baseUrl}/${id}`);
    }
}