import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessoresAlunosComponent } from './professores-alunos.component';

describe('ProfessoresAlunosComponent', () => {
  let component: ProfessoresAlunosComponent;
  let fixture: ComponentFixture<ProfessoresAlunosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfessoresAlunosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfessoresAlunosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
