﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartSchool.API.Models
{
    public class AlunoDisciplina
    {
        public AlunoDisciplina()
        {

        }

        public AlunoDisciplina(int alunoId, int disciplinaId)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
        }

        public int AlunoId { get; set; }

        //[JsonIgnore]
        public Aluno Aluno { get; set; }

        public int DisciplinaId { get; set; }

        public Disciplina Disciplina { get; set; }
    }
}
