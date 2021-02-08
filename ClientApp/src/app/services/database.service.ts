import { Injectable } from "@angular/core";
import { Connection } from "../models/Connection";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {
  connection: Connection;
  route: string;
  json = 'json/';

  constructor(private http: HttpClient) {
    this.connection = new Connection();
  }

  SetRoute(_path: string) {
    this.route = 'api/' + _path;
  }

  AddObj<T>(_obj: any): Observable<T> {
    return this.http.post<T>(this.route, _obj);
  }

  GetObjList<T>(): Observable<T> {
    return this.http.get<T>(this.route);
  }

  UpdateObj<T>(_obj: any): Observable<T> {
    return this.http.put<T>(this.route + `/${_obj.id}`, _obj);
  }

  Update<T>(_obj: any): Observable<T> {
    return this.http.put<T>(this.route, _obj);
  }

  DeleteObj<T>(_obj: any): Observable<any> {
    return this.http.delete<T>(this.route + `/${_obj.id}`);
  }

  Delete<T>(): Observable<any> {
    return this.http.delete<T>(this.route);
  }
}
