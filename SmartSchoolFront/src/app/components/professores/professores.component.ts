import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Disciplina } from 'src/app/models/Disciplina';
import { Professor } from 'src/app/models/Professor';
import { ProfessorService } from 'src/app/services/professor.service';
import { EnumHttpMethod } from 'src/app/util/enumHttpMethod';
import { Util } from 'src/app/util/util';

@Component({
  selector: 'app-professores',
  templateUrl: './professores.component.html',
  styleUrls: ['./professores.component.scss']
})
export class ProfessoresComponent implements OnInit {

  public modalRef: BsModalRef;
  public professorForm: FormGroup;
  public titulo = 'Professores';
  public professorSelecionado: Professor;
  public professores: Professor[] = [];
  public professor: Professor;
  public modeSaveTeste: EnumHttpMethod = EnumHttpMethod.POST;

  private unsubscriber = new Subject();

  constructor(
    //private alunoService: AlunoService,
    private route: ActivatedRoute,
    private professorService: ProfessorService,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private location:Location
  ) {
    this.criarForm()
  }

  criarForm(): void {
    this.professorForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      //sobrenome: ['', Validators.required],
      disciplinas: []
    });
  }



  ngOnInit() {
    this.carregarProfessores();
  }

  carregarProfessores(): void {
    const profId: string | null= this.route.snapshot.paramMap.get('id');

    this.spinner.show();
    this.professorService.getAll()
    .pipe(takeUntil(this.unsubscriber))
    .subscribe({
      next:(professores: Professor[])=>{
        this.professores = professores;
        if(profId != null && Number.parseInt(profId) > 0){
          this.professorSelect(Number.parseInt(profId));
        }

        this.toastr.success('Professores foram carregados com Sucesso!');
      },
      error: (err)=>{
        this.toastr.error('Professores não carregados!');
          console.error(err);
          this.spinner.hide();

      },
      complete:() => this.spinner.hide()
    });
  };

  trocarEstado(professor: Professor){
    professor.ativo = !professor.ativo;
    this.professorService.trocarEstado(professor.id, professor.ativo)
    .pipe(takeUntil(this.unsubscriber))
    .subscribe({
      next: (resp:any)=>{
        console.log(`resposta: ${resp.message}`);
          this.toastr.success(resp.message);
      },
      error: (error: any) => {
        this.toastr.error(`Erro: Professor não pôde ser salvo!`);
        console.error(error);
        this.spinner.hide();
      },
      complete: () => this.spinner.hide()
    });
  }

  professorSelect(professorId: number){
    this.modeSaveTeste = EnumHttpMethod.PATCH;

    this.professorService.getById(professorId).subscribe(
      {
        next:(professorReturn) => {
          this.professorSelecionado = professorReturn;
          this.professorForm.patchValue(this.professorSelecionado);
        },
        error:(error) => {
          this.toastr.error('Professor não carregado!');
          console.error(error);
          this.spinner.hide();
        },
        complete:() => this.spinner.hide()
      }
    );
  }

  disciplinaConcat(disciplinas: Disciplina[]) {
    return Util.nomeConcat(disciplinas);
  }

}
