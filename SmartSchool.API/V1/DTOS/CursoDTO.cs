using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.DTOS
{
    public class CursoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<DisciplinaDTO> Disciplinas { get; set; }
    }
}
