using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.DTOS
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }        
        public string Telefone { get; set; }
        public int Idade { get; set; }//data atual - data de nascimento aluno
        public DateTime DataIni { get; set; }        
        public bool Ativo { get; set; }
        


    }
}
