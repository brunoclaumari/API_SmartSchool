using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Models
{
    public class Professor : EntidadeGenerica
    {
        public Professor()
        {

        }

        public Professor(int id,string nome)
        {
            Id = id;
            Nome = nome;            
        }

        public string Nome { get; set; }        
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}
