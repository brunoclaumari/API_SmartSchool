using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.DTOS
{
    /// <summary>
    /// DTO de Aluno para registro (atributos passados no body)
    /// </summary>
    public class AlunoRegistrarDTO
    {
        /// <summary>
        /// Identificador e chave do banco
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Número da matrícula do aluno
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        /// Nome do aluno
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Sobre nome do aluno
        /// </summary>
        public string Sobrenome { get; set; }
        /// <summary>
        /// Telefone do aluno
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Data de nascimento do aluno
        /// </summary>
        public DateTime DataNasc { get; set; }
        /// <summary>
        /// Data de início de cadastro do aluno
        /// </summary>
        public DateTime DataIni { get; set; } = DateTime.Now;

        /// <summary>
        /// Data do encerramento do cadastro do aluno
        /// </summary>
        public DateTime? DataFim { get; set; } = null;
        /// <summary>
        /// Flag de status do aluno
        /// </summary>
        public bool Ativo { get; set; } = true;
    }
}
