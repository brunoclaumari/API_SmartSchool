using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Models
{
    public class Aluno : EntidadeGenerica
    {
        public Aluno()
        {

        }

        public Aluno(int id, string nome, string sobrenome, string telefone)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
        }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }

        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
 

        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome}, Sobrenome: {Sobrenome}, Telefone: {Telefone}";
        }
    }
}
