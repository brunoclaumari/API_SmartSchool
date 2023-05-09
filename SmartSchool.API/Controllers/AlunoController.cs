using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private List<Aluno> ListaAlunos;

        private const string Alunos = "Alunos";

        public AlunoController()
        {
            ListaAlunos = new List<Aluno>();
            //ManipulaDadosSessao();
        }
         
        private void ManipulaDadosSessao()
        {
            string dados = string.Empty;
            try
            {
                dados = HttpContext.Session.GetString(Alunos);
                if (!string.IsNullOrEmpty(dados))
                    ListaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(dados);
                else
                    InicializaLista(dados);
            }
            catch (Exception e)
            {
                InicializaLista(dados);
            }
        }

        private void InicializaLista(string dados)
        {            
            SeedTemporario seed = new SeedTemporario();
            this.ListaAlunos = seed.ListaAlunos;
            dados = JsonConvert.SerializeObject(ListaAlunos);
            HttpContext.Session.SetString(Alunos, dados);
        }

        private void SalvaDadosSessao()
        {
            HttpContext.Session.SetString(Alunos, JsonConvert.SerializeObject(ListaAlunos));
        }


        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            ManipulaDadosSessao();
            return Ok(ListaAlunos);
        }

        // GET api/<AlunoController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            ManipulaDadosSessao();
            Aluno aluno = ListaAlunos.FirstOrDefault(x => x.Id == id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");

            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            ManipulaDadosSessao();
            Aluno aluno = ListaAlunos.FirstOrDefault(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome));

            if (aluno is null) return BadRequest($"Aluno nome completo {nome} {sobrenome} não encontrado!!");

            return Ok(aluno);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post([FromBody] Aluno value)
        {
            ManipulaDadosSessao();
            int ultimoId = ListaAlunos.LastOrDefault().Id;
            value.Id = ultimoId + 1;
            ListaAlunos.Add(value);
            SalvaDadosSessao();

            return Ok(value);
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aluno value)
        {
            ManipulaDadosSessao();
            Aluno aluno = ListaAlunos.FirstOrDefault(x => x.Id == id);
            int indice = ListaAlunos.IndexOf(aluno);
            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            value.Id = aluno.Id;
            ListaAlunos[indice] = value;
            SalvaDadosSessao();

            return Ok(value);
        }

        // PUT api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Aluno value)
        {
            ManipulaDadosSessao();

            return Ok(value);
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ManipulaDadosSessao();
            Aluno aluno = ListaAlunos.FirstOrDefault(x => x.Id == id);
            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            ListaAlunos.Remove(aluno);
            SalvaDadosSessao();

            return NoContent();
        }
    }
}
