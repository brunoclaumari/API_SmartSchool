﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.V1.DTOS;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// Controller para entidade Professor versão 1.0
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {        
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna ProfessorRegistrarDTO apenas para teste
        /// </summary>
        /// <returns></returns>
        [HttpGet("teste")]
        public IActionResult GetTeste()
        {
            return Ok(new ProfessorRegistrarDTO());
        }

        /// <summary>
        /// Método que retorna todos os Professores
        /// </summary>
        /// <returns></returns>
        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            var prof = _repo.GetAllProfessores(true);
            var profDTO = _mapper.Map<IEnumerable<ProfessorDTO>>(prof);

            return Ok(profDTO);
        }

        /// <summary>
        /// Método que retorna um Professor por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<ProfessorController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            Professor prof = _repo.GetProfessorById(id, true);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");

            return Ok(_mapper.Map<ProfessorDTO>(prof));
        }

        /// <summary>
        /// Retorna lista de professores buscando pelo id do aluno
        /// </summary>
        /// <param name="alunoId"></param>
        /// <returns></returns>
        [HttpGet("byAluno/{alunoId}")]
        public IActionResult GetProfessoresByAlunoId(int alunoId)
        {
            List<Professor> profs = _repo.GetProfessoresByAlunoId(alunoId, true);

            if (profs is null) return BadRequest($"Professor não encontrados!!");

            return Ok(_mapper.Map<IEnumerable<ProfessorDTO>>(profs));
        }

        //[HttpGet("byName")]
        //public IActionResult GetByName(string nome)
        //{
        //    Professor prof = _smartContext.Professores.FirstOrDefault(x => x.Nome.Contains(nome));

        //    if (prof is null) return BadRequest($"Professor nome: {nome} não encontrado!!");

        //    return Ok(prof);
        //}

        /// <summary>
        /// Método para cadastrar um novo Professor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post([FromBody] ProfessorRegistrarDTO model)
        {
            Professor prof = _mapper.Map<Professor>(model);
            
            _repo.Add(prof);
            if (_repo.SaveChanges())
                return Created($"api/professor/{prof.Id}", _mapper.Map<ProfessorDTO>(prof));
            else
                return BadRequest("Ocorreu um erro ao inserir o Professor");
        }

        /// <summary>
        /// Método que atualiza um Professor (obrigatório todos os atributos de Professor)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProfessorRegistrarDTO model)
        {
            Professor prof = _repo.GetProfessorById(id);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");
            _mapper.Map(model, prof);
            _repo.Update(prof);
            if (_repo.SaveChanges())
                return Ok(_mapper.Map<ProfessorDTO>(prof));
            else
                return BadRequest("Ocorreu um erro ao atualizar o Professor");
            
        }

        /// <summary>
        /// Método que atualiza um Professor (não precisa de todos os atributos de Professor)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // PATCH api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] ProfessorRegistrarDTO model)
        {
            Professor prof = _repo.GetProfessorById(id);

            if (prof is null) return BadRequest($"Professor id = {id} não encontrado!!");
            _mapper.Map(model, prof);
            _repo.Update(prof);
            if (_repo.SaveChanges())
                return Ok(_mapper.Map<ProfessorDTO>(model));
            else
                return BadRequest("Ocorreu um erro ao atualizar o Professor");
        }

        /// <summary>
        /// Troca estado de um professor buscando por Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trocaEstadoDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}/trocarEstado")]
        public IActionResult TrocarEstado(int id, [FromBody] TrocaEstadoDTO trocaEstadoDTO)
        {            
            Professor professor = _repo.GetProfessorById(id);

            if (professor is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            professor.Ativo = trocaEstadoDTO.Estado;
            
            _repo.Update(professor);

            if (_repo.SaveChanges()){
                string msg = professor.Ativo? "ativado" : "desativado";
            //return Ok(_mapper.Map<AlunoPatchDTO>(aluno));
                return Ok(new {message = $"Professor {msg} com sucesso!!"});
            }
            else
                return BadRequest($"Falha na atualização do professor id: {id}");
        }

        /// <summary>
        /// Deleta um Professor pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
