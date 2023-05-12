using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _smartContext;

        public ProfessorController(SmartContext smartContext)
        {
            _smartContext = smartContext;
        }




        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            var prof = _smartContext.Professores;

            return Ok(prof);
        }

        // GET api/<ProfessorController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            Professor prof = _smartContext.Professores.FirstOrDefault(x => x.Id == id);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");

            return Ok(prof);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome)
        {
            Professor prof = _smartContext.Professores.FirstOrDefault(x => x.Nome.Contains(nome));

            if (prof is null) return BadRequest($"Professor nome: {nome} não encontrado!!");

            return Ok(prof);
        }

        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post([FromBody] Professor value)
        {
            _smartContext.Professores.Add(value);
            _smartContext.SaveChanges();

            return CreatedAtAction("Post", value);
        }

        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor value)
        {
            Professor prof = _smartContext.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");
            value.Id = prof.Id;
            _smartContext.Professores.Update(value);
            _smartContext.SaveChanges();

            return Ok(value);
        }

        // PATCH api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Professor value)
        {
            Professor prof = _smartContext.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");
            value.Id = prof.Id;
            _smartContext.Professores.Update(value);
            _smartContext.SaveChanges();

            return Ok(value);
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Professor prof = _smartContext.Professores.FirstOrDefault(x => x.Id == id);
            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");

            _smartContext.Professores.Remove(prof);
            _smartContext.SaveChanges();

            return NoContent();
        }
    }
}
