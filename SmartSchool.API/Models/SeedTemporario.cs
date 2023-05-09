using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Models
{
    public class SeedTemporario
    {
        public List<Aluno> ListaAlunos;

        public SeedTemporario()
        {            
            PreencheLista();
        }

        private void PreencheLista()
        {
            ListaAlunos = new List<Aluno>()
            {
                new Aluno()
                {
                    Id = 1,
                    Nome = "Marcos",
                    Sobrenome="Almeida",
                    Telefone = "45669877"
                },
                new Aluno()
                {
                    Id = 2,
                    Nome = "Marta",
                    Sobrenome="Kent",
                    Telefone = "58987555"
                },
                new Aluno()
                {
                    Id = 3,
                    Nome = "Laura",
                    Sobrenome="de Sousa",
                    Telefone = "123654855"
                },
            };
        }
    }
}
