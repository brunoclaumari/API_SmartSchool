using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartSchool.API.Data;
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

        private const string Alunos = "Alunos";
        private readonly SmartContext _smartContext;

        public AlunoController(SmartContext smartContext)
        {
            _smartContext = smartContext;
        }

        private void SalvaDadosSessao()
        {
            //HttpContext.Session.SetString(Alunos, JsonConvert.SerializeObject(ListaAlunos));
        }


        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            var lista = _smartContext.Alunos;

            return Ok(lista);
        }

        // GET api/<AlunoController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            
            Aluno aluno = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");

            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            Aluno aluno =  _smartContext.Alunos.FirstOrDefault(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome));

            if (aluno is null) return BadRequest($"Aluno nome completo {nome} {sobrenome} não encontrado!!");

            return Ok(aluno);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post([FromBody] Aluno value)
        {
            _smartContext.Alunos.Add(value);
            _smartContext.SaveChanges();

            return CreatedAtAction("Post", value);
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aluno value)
        {
            Aluno aluno = _smartContext.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            value.Id = aluno.Id;
            _smartContext.Alunos.Update(value);
            _smartContext.SaveChanges();

            return Ok(value);
        }

        // PATCH api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Aluno value)
        {
            Aluno aluno = _smartContext.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            value.Id = aluno.Id;
            _smartContext.Alunos.Update(value);
            _smartContext.SaveChanges();

            return Ok(value);
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Aluno aluno = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);
            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");

            _smartContext.Alunos.Remove(aluno);
            _smartContext.SaveChanges();

            return NoContent();
        }
    }
}
