import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessorDetalheComponent } from './professor-detalhe.component';

describe('ProfessorDetalheComponent', () => {
  let component: ProfessorDetalheComponent;
  let fixture: ComponentFixture<ProfessorDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfessorDetalheComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfessorDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
