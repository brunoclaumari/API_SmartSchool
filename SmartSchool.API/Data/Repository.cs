﻿using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository(SmartContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : EntidadeGenerica
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : EntidadeGenerica
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : EntidadeGenerica
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Aluno> GetAllAlunos(bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                //"Include" e "ThenInclude" equivalem a fazer Inner joins nas tabelas
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor)
                             ;
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToList();
        }

        public List<Aluno> GetAllAlunosByDisciplinaId(int disciplinaId, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor)
                             ;
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(al => al.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToList();
        }

        public Aluno GetAlunoById(int alunoId, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor)
                             ;
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(al => al.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Aluno GetAlunoByName(string nome, string sobrenome, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor)
                             ;
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(al => al.Nome == nome && al.Sobrenome == sobrenome);

            return query.FirstOrDefault();
        }

        public List<Professor> GetAllProfessores(bool incluirAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (incluirAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno)
                             ;
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToList();
        }

        public Professor GetProfessorById(int professorId, bool incluirAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (incluirAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno)
                             ;
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(p => p.Id == professorId);

            return query.FirstOrDefault();
        }

        public List<Professor> GetProfessoresByDisciplinaId(int disciplinaId, bool incluirAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (incluirAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno)
                             ;
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(al => al.Disciplinas.Any(
                             d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));

            return query.ToList();
        }
    }
}
