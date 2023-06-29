using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : EntidadeGenerica;

        void Update<T>(T entity) where T : EntidadeGenerica;

        void Delete<T>(T entity) where T : EntidadeGenerica;

        bool SaveChanges();

        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool incluirProfessor = false);


        List<Aluno> GetAllAlunos(bool incluirProfessor = false);

        Task<List<Aluno>> GetAllAlunosByDisciplinaId(int disciplinaId, bool incluirProfessor = false);

        Task<Aluno> GetAlunoByIdAsync(int alunoId, bool incluirProfessor = false);        

        List<Professor> GetAllProfessores(bool incluirAluno = false);

        Professor GetProfessorById(int professorId, bool incluirAluno = false);

        List<Professor> GetProfessoresByDisciplinaId(int disciplinaId, bool incluirAluno = false);

        List<Professor> GetProfessoresByAlunoId(int alunoId, bool incluirAlunoEDiscplina = false);



    }
}
