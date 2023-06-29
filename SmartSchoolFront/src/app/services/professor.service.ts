import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Professor } from '../models/Professor';
import { Observable } from 'rxjs';
import { environment } from '../../../src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfessorService {

  baseURL = `${environment.mainUrlAPI}professor`;

  constructor(private http: HttpClient) { }

  trocarEstado(professorId: number, ativo: boolean) {
    return this.http.patch(`${this.baseURL}/${professorId}/trocarEstado`, { Estado: ativo });
  }

  getAll(): Observable<Professor[]> {
    return this.http.get<Professor[]>(this.baseURL);
  }

  getById(id: number): Observable<Professor> {
    return this.http.get<Professor>(`${this.baseURL}/byId/${id}`);
  }

  getByAlunoId(id: number): Observable<Professor[]> {
    return this.http.get<Professor[]>(`${this.baseURL}/ByAluno/${id}`);
  }

  post(professor: Professor) {
    return this.http.post(this.baseURL, Professor);
  }

  put(professor: Professor) {
    return this.http.put(`${this.baseURL}/${professor.id}`, Professor);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }


}
