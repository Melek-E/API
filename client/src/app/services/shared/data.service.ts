import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import {
  map,
  groupBy,
  mergeMap,
  toArray,
} from 'rxjs/operators';

import {IUser} from "./auth.service";
import { Test } from '../../types/test';
import { Answer } from '../../types/answer';
import { Framework } from '../../types/Framework';
import { Question } from '../../types/questions';


const API_URL = 'http://localhost:7112/api';

@Injectable()
export class DataService {
  constructor(private http: HttpClient) {}




  public getTests = (): Observable<Test[]> =>
    this.http.get<Test[]>(`${API_URL}/Tests`);

  public getTest = (id: string): Observable<Test> =>
    this.http.get<Test>(`${API_URL}/Tests/Users/${id}`);

  public getAnswers = (): Observable<Answer[]> =>
    this.http.get<Answer[]>(`${API_URL}/Answers`);
  public getAnswer = (id: number) =>
    this.http.get(`${API_URL}/Answers/${id}`);


  public getFrameworks = (): Observable<Framework[]> =>
    this.http.get<Framework[]>(`${API_URL}/Frameworks`);

  public getFramework = (id: number) =>
    this.http.get(`${API_URL}/Frameworks/${id}`);

  public getSelectedFrameworks = (): Observable<Framework[]> =>
    this.http.get<Framework[]>(`${API_URL}/Frameworks/selected`);


  public getQuestions = (): Observable<Question[]> =>
    this.http.get<Question[]>(`${API_URL}/Questions`);
  public getQuestion= (id: number) =>
    this.http.get(`${API_URL}/Questions/${id}`);

  public getUsers = (): Observable<IUser[]> =>
    this.http.get<IUser[]>(`${API_URL}/Users`);
  public getUser = (id: number) =>
    this.http.get(`${API_URL}/Users/${id}`);



  public getAllUsersThatAreAdmins = (): Observable<IUser[]> =>
    this.http.get<IUser[]>(`${API_URL}/Users/Admins`);

  public getAllUsersThatAreUsers = (): Observable<IUser[]> =>
    this.http.get<IUser[]>(`${API_URL}/Users/AllUsers`);



}
