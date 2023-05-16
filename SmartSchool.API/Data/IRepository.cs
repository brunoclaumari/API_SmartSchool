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

        List<Aluno> GetAllAlunos(bool incluirProfessor = false);

        List<Aluno> GetAllAlunosByDisciplinaId(int disciplinaId, bool incluirProfessor = false);

        Aluno GetAlunoById(int alunoId, bool incluirProfessor = false);        

        List<Professor> GetAllProfessores(bool incluirAluno = false);

        Professor GetProfessorById(int professorId, bool incluirAluno = false);

        List<Professor> GetProfessoresByDisciplinaId(int disciplinaId, bool incluirAluno = false);



    }
}
