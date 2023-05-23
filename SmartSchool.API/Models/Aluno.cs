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

        public Aluno(int id,
                     int matricula,
                     string nome,
                     string sobrenome,
                     string telefone,
                     DateTime dataNasc)
        {
            this.Id = id;
            this.Matricula = matricula;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.DataNasc = dataNasc;
        }

        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }


        public override string ToString()
        {
            return $"Id: {Id}, " +
                $"Nome: {Nome}, Sobrenome: {Sobrenome}, " +
                $"Telefone: {Telefone}, Data nascimento: {DataNasc}, " +
                $"Aluno ativo: {Ativo}, Cadastrado em: {DataIni}";
        }
    }
}
