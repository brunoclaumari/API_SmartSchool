
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Aluno } from '../models/Aluno';

import { environment } from 'src/environment/environment';
import { PaginatedResult } from '../models/Pagination';
import { map, repeat } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  baseURL = `${environment.mainUrlAPI}aluno`;

  constructor(private http: HttpClient) { }

  getAll(page?: number, itemsPerPage?: number ): Observable<PaginatedResult<Aluno[]>> {
    const paginatedResult: PaginatedResult<Aluno[]> = new PaginatedResult<Aluno[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<Aluno[]>(this.baseURL, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body??[];
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')??'');
          }
          return paginatedResult;
        })
      );
  }

  getById(id: number): Observable<Aluno> {
    return this.http.get<Aluno>(`${this.baseURL}/byId/${id}`);
  }

  getByDisciplinaId(id: number): Observable<Aluno[]> {
    return this.http.get<Aluno[]>(`${this.baseURL}/ByDisciplina/${id}`);
  }

  post(aluno: Aluno) {
    console.log("post");
    return this.http.post(this.baseURL, aluno);
  }

  save(aluno :Aluno, modeSave: String ){

/*     switch(modeSave){
      case 'put':
        return this.put(aluno);
      case 'patch':
        return this.patch(aluno);
      default:
        return this.post(aluno);
    } */
    console.log(modeSave);
    if(modeSave === 'put'){
      return this.put(aluno);
    }
    else if(modeSave === 'patch'){
      return this.patch(aluno);
    }
    else{
      return this.post(aluno);
    }

  }

  put(aluno: Aluno) {
    console.log("put");
    return this.http.put(`${this.baseURL}/${aluno.id}`, aluno);
  }

  trocarEstado(alunoId: number, ativo: boolean) {
    return this.http.patch(`${this.baseURL}/${alunoId}/trocarEstado`, { Estado: ativo });
    //return this.http.patch(`${this.baseURL}/${alunoId}/trocarEstado`, ativo);
  }

  patch(aluno: Aluno) {
    console.log("patch");
    return this.http.patch(`${this.baseURL}/${aluno.id}`, aluno);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}
