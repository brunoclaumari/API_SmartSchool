using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartSchool.API.Data;
using SmartSchool.API.DTOS;
using SmartSchool.API.Helpers;
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
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {            
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {            
            var listaAlunos = _repo.GetAllAlunos(true);            

            var enumerableRetorno = _mapper.Map<IEnumerable<AlunoDTO>>(listaAlunos);

            return Ok(enumerableRetorno);
        }

        [HttpGet("teste")]
        public IActionResult GetTeste()
        {   
            return Ok(new AlunoRegistrarDTO());
        }

        // GET api/<AlunoController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {

            //Aluno aluno = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);
            Aluno aluno = _repo.GetAlunoById(id, true);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return Ok(alunoDTO);
        }
        

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post([FromBody] AlunoRegistrarDTO model)
        {
            Aluno aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
                return Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDTO>(aluno));
            else
                return BadRequest("Falha ao salvar Aluno.");
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AlunoRegistrarDTO model)
        {            
            Aluno aluno = _repo.GetAlunoById(id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            //value.Id = aluno.Id;
            _mapper.Map(model, aluno);
            _repo.Update(aluno);

            if (_repo.SaveChanges())
                return Ok(_mapper.Map<AlunoDTO>(aluno));
            else
                return BadRequest($"Falha na atualização do Aluno id: {id}");
        }

        // PATCH api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] AlunoRegistrarDTO model)
        {            
            Aluno aluno = _repo.GetAlunoById(id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            //value.Id = aluno.Id;
            _mapper.Map(model, aluno);
            _repo.Update(aluno);

            if (_repo.SaveChanges())
                return Ok(_mapper.Map<AlunoDTO>(aluno));
            else
                return BadRequest($"Falha na atualização do Aluno id: {id}");
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Aluno aluno = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);
            Aluno aluno = _repo.GetAlunoById(id);
            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");

            _repo.Delete(aluno);

            if (_repo.SaveChanges())
                return NoContent();
            else
                return BadRequest($"Não foi possível excluir o Aluno id: {id}");

            //return NoContent();
        }
    }
}
