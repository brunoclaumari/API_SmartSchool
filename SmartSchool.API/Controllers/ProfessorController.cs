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
        private readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }


        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            var prof = _repo.GetAllProfessores(true);

            return Ok(prof);
        }

        // GET api/<ProfessorController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            Professor prof = _repo.GetProfessorById(id);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");

            return Ok(prof);
        }

        //[HttpGet("byName")]
        //public IActionResult GetByName(string nome)
        //{
        //    Professor prof = _smartContext.Professores.FirstOrDefault(x => x.Nome.Contains(nome));

        //    if (prof is null) return BadRequest($"Professor nome: {nome} não encontrado!!");

        //    return Ok(prof);
        //}

        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post([FromBody] Professor value)
        {
            _repo.Add(value);
            if (_repo.SaveChanges())
                return CreatedAtAction("Post", value);
            else
                return BadRequest("Ocorreu um erro ao inserir o Professor");
        }

        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor value)
        {
            Professor prof = _repo.GetProfessorById(id);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");
            value.Id = prof.Id;
            _repo.Update(value);
            if (_repo.SaveChanges())
                return Ok(value);
            else
                return BadRequest("Ocorreu um erro ao atualizar o Professor");
            
        }

        // PATCH api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Professor value)
        {
            Professor prof = _repo.GetProfessorById(id);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");
            value.Id = prof.Id;
            _repo.Update(value);
            if (_repo.SaveChanges())
                return Ok(value);
            else
                return BadRequest("Ocorreu um erro ao atualizar o Professor");
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Professor prof = _repo.GetProfessorById(id);
            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");

            _repo.Delete(prof);

            if (_repo.SaveChanges())
                return NoContent();
            else
                return BadRequest($"Não foi possível excluir o Professor id: {id}");
        }
    }
}
