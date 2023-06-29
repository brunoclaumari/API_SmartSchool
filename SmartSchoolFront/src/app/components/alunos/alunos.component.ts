
import { Component, OnInit, OnDestroy, TemplateRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, of } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { PaginatedResult, Pagination } from 'src/app/models/Pagination';

import { Aluno } from '../../models/Aluno';
import { Professor } from '../../models/Professor';

import { AlunoService } from '../../services/aluno.service';
import { ProfessorService } from '../../services/professor.service';
import { Location } from '@angular/common';
import { EnumHttpMethod } from 'src/app/util/enumHttpMethod';

@Component({
  selector: 'app-alunos',
  templateUrl: './alunos.component.html',
  styleUrls: ['./alunos.component.scss']
})

export class AlunosComponent implements OnInit, OnDestroy {

  public modalRef: BsModalRef;
  public alunoForm: FormGroup;
  public titulo = 'Alunos';
  public alunoSelecionado: Aluno;
  public profsAlunos: Professor[];
  public alunos?: Aluno[] = [];
  public aluno: Aluno;
  public modeSave: String = 'post';
  public modeSaveTeste: EnumHttpMethod = EnumHttpMethod.POST;

  /* public modeSave: ModeSave = {
    requisicao: 'post'
  } ; */
  public msnDeleteAluno: string;
  pagination: Pagination;

  private unsubscriber = new Subject();

  constructor(
    private alunoService: AlunoService,
    private route: ActivatedRoute,
    private professorService: ProfessorService,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private location:Location
  ) {
    this.criarForm();
  }

  ngOnInit(): void {
    this.pagination = { currentPage: 1, itemsPerPage: 4} as Pagination;
    this.carregarAlunos();
  }

  professoresAlunos(template: TemplateRef<any>, id: number): void {
    this.spinner.show();
    this.professorService.getByAlunoId(id)
    .pipe(takeUntil(this.unsubscriber))
    .subscribe({
      //next: () => this.nextProfessor(template),
      next: (professores: Professor[]) => {
        this.profsAlunos = professores;
        this.modalRef = this.modalService.show(template);
      },
      error: (error: any) => {
        this.toastr.error(`erro: ${error.message}`);
        console.error(error.message);
        this.spinner.hide();
      },
      complete: () => this.spinner.hide()
    });

  }

  criarForm(): void {
    this.alunoForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      telefone: ['', Validators.required],
      ativo: []
    });
  }

  trocarEstado(aluno: Aluno) {
    aluno.ativo = !aluno.ativo;
    console.log(aluno.ativo);
    this.alunoService.trocarEstado(aluno.id, aluno.ativo)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe({
        next: (resp:any) => {
          console.log(`resposta: ${resp.message}`);
          this.toastr.success(resp.message);

        },
        error: (error: any) => {
          this.toastr.error(`Erro: Aluno não pode ser salvo!`);
          console.error(error);
          this.spinner.hide();
        },
        complete: () => this.spinner.hide()
      }
      );
  }

  saveAluno(): void {
    if (this.alunoForm.valid) {
      this.spinner.show();

      //if (this.modeSave === 'post') {
      if (this.modeSaveTeste === EnumHttpMethod.POST) {
        this.aluno = {...this.alunoForm.value};
      } else {
        this.aluno = {id: this.alunoSelecionado.id, ...this.alunoForm.value};
      }


      //this.alunoService.save(this.aluno, this.modeSave)
      this.alunoService[this.modeSaveTeste](this.aluno)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(
          {
          next: () => {
            //this.carregarAlunos();
            this.ngOnInit();
            this.toastr.success('Aluno salvo com sucesso!');
          },
          error:(error: any) => {
            this.toastr.error(`Erro: Aluno não pode ser salvo!`);
            console.error(error);
            this.spinner.hide();
          },
          complete: () => this.spinner.hide()
        }
      );
    }
  }

  carregarAlunos(carregaToastr:boolean = true): void {
    const alunoId: string | null = this.route.snapshot.paramMap.get('id');

    //console.log(this.pagination);
    this.spinner.show();
    this.alunoService.getAll(this.pagination.currentPage, this.pagination.itemsPerPage)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe({
        next: (alunos: PaginatedResult<Aluno[]>) => {
          this.alunos = alunos.result;
          this.pagination = alunos.pagination as Pagination;

          if(alunoId != null ){
            if (Number.parseInt(alunoId) > 0) {
              this.alunoSelect(Number.parseInt(alunoId));
            }
          }
          if(carregaToastr)
            this.toastr.success('Alunos foram carregado com Sucesso!');
        },
        error: (error: any) => {
          this.toastr.error('Alunos não carregados!');
          console.error(error);
          this.spinner.hide();
        },
        complete: () => this.spinner.hide()
      }
    );
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.carregarAlunos();
  }

  incrementNumberPerPage(event: any): void{
    //console.log(event);
    //this.pagination.itemsPerPage = event;

    this.carregarAlunos(false);
  }

  alunoSelect(alunoId: number): void {
    //this.modeSave = 'patch';
    this.modeSaveTeste = EnumHttpMethod.PATCH;

    this.alunoService.getById(alunoId).subscribe(
      {
        next:(alunoReturn) => {
          this.alunoSelecionado = alunoReturn;
          this.alunoForm.patchValue(this.alunoSelecionado);
        },
        error:(error) => {
          this.toastr.error('Alunos não carregados!');
          console.error(error);
          this.spinner.hide();
        },
        complete:() => this.spinner.hide()
      }
    );
  }

  voltar(): void {
    //this.alunoSelecionado = [];
    this.location.back();
  }

  openModal(template: TemplateRef<any>, alunoId: number): void {
    this.professoresAlunos(template, alunoId);
  }

  closeModal(): void {
    this.modalRef.hide();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next("");
    this.unsubscriber.complete();
  }

}
