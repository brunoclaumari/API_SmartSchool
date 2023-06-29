import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AlunosComponent } from './components/alunos/alunos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { ProfessoresComponent } from './components/professores/professores.component';
import { NavComponent } from './components/shared/nav/nav.component';
import { TituloComponent } from './components/shared/titulo/titulo.component';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TesteeComponent } from './testee/testee.component'
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfessoresAlunosComponent } from './components/alunos/professores-alunos/professores-alunos.component';
import { ProfessorDetalheComponent } from './components/professores/professor-detalhe/professor-detalhe.component';
import { AlunosProfessoresComponent } from './components/professores/alunos-professores/alunos-professores.component';

@NgModule({
  declarations: [
    AppComponent,
    AlunosComponent,
    ProfessoresComponent,
    ProfessoresAlunosComponent,
    AlunosProfessoresComponent,
    PerfilComponent,
    DashboardComponent,
    NavComponent,
    TituloComponent,
    TesteeComponent,
    ProfessoresAlunosComponent,
    ProfessorDetalheComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,//para trabalhar com formulários
    ReactiveFormsModule,//trabalhar com formulários reativos
    HttpClientModule,//para realizar requisições HTTP do client para a API
    BrowserAnimationsModule,
    NgxSpinnerModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3500,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
      closeButton: true
    })
  ],
  providers: [FormsModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
