using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.V1.DTOS;
using SmartSchool.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.API.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// Controller da entidade Aluno versão 1.0
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {        

        private const string Alunos = "Alunos";        
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {            
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Método que retorna todos os Alunos
        /// </summary>
        /// <returns></returns>
        // GET: api/<AlunoController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {            
            var listaAlunos = await _repo.GetAllAlunosAsync(pageParams, true);            

            var alunosResult = _mapper.Map<IEnumerable<AlunoDTO>>(listaAlunos);

            Response.AddPagination(listaAlunos.CurrentPage,listaAlunos.PageSize,listaAlunos.TotalCount,listaAlunos.TotalPages);

            return Ok(alunosResult);
        }

        /// <summary>
        /// Retorna um "AlunoRegistrarDTO" para testar JSON
        /// </summary>
        /// <returns></returns>
        [HttpGet("teste")]
        public IActionResult GetTeste()
        {   
            return Ok(new AlunoRegistrarDTO());
        }

        /// <summary>
        /// Método que retorna Aluno por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<AlunoController>/5
        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            //Aluno aluno = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);
            Aluno aluno = await _repo.GetAlunoByIdAsync(id, true);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            var alunoDTO = _mapper.Map<AlunoRegistrarDTO>(aluno);

            return Ok(alunoDTO);
        }

        /// <summary>
        /// Retorna lista de Alunos pelo id da disciplina
        /// </summary>
        /// <param name="disciplinaId"></param>
        /// <returns></returns>
        [HttpGet("byDisciplina/{disciplinaId}")]
        public async Task<IActionResult> GetByDisciplina(int disciplinaId)
        {

            //Aluno aluno = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);
            IEnumerable<Aluno> alunos =  await _repo.GetAllAlunosByDisciplinaId(disciplinaId, true);

            if (alunos is null) return BadRequest($"Aluno pela disciplina id = {disciplinaId} não encontrado!!");
            var alunosDTO = _mapper.Map<IEnumerable<AlunoRegistrarDTO>>(alunos);

            return Ok(alunosDTO);
        }
        

        /// <summary>
        /// Método para cadastrar um novo Aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método que atualiza um Aluno (obrigatório todos os atributos de Aluno)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AlunoRegistrarDTO model)
        {            
            Aluno aluno = await _repo.GetAlunoByIdAsync(id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            //value.Id = aluno.Id;
            _mapper.Map(model, aluno);
            _repo.Update(aluno);

            if (_repo.SaveChanges())
                return Ok(_mapper.Map<AlunoDTO>(aluno));
            else
                return BadRequest($"Falha na atualização do Aluno id: {id}");
        }

        /// <summary>
        /// Método que atualiza um Aluno (não precisa de todos os atributos de Aluno)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // PATCH api/<AlunoController>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] AlunoPatchDTO model)
        {            
            Aluno aluno = await _repo.GetAlunoByIdAsync(id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            //value.Id = aluno.Id;
            _mapper.Map(model, aluno);
            _repo.Update(aluno);

            if (_repo.SaveChanges())
                return Ok(_mapper.Map<AlunoPatchDTO>(aluno));
            else
                return BadRequest($"Falha na atualização do Aluno id: {id}");
        }

        /// <summary>
        /// Troca estado de um aluno buscando por Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trocaEstadoDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}/trocarEstado")]
        public async Task<IActionResult> TrocarEstado(int id, [FromBody] TrocaEstadoDTO trocaEstadoDTO)
        {            
            Aluno aluno = await _repo.GetAlunoByIdAsync(id);

            if (aluno is null) return BadRequest($"Aluno id = {id} não encontrado!!");
            aluno.Ativo = trocaEstadoDTO.Estado;
            
            _repo.Update(aluno);

            if (_repo.SaveChanges()){
                string msg = aluno.Ativo? "ativado" : "desativado";
            //return Ok(_mapper.Map<AlunoPatchDTO>(aluno));
                return Ok(new {message = $"Aluno {msg} com sucesso!!"});
            }
            else
                return BadRequest($"Falha na atualização do Aluno id: {id}");
        }

        /// <summary>
        /// Deleta um Aluno pelo 'id'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //Aluno aluno = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);
            Aluno aluno = await _repo.GetAlunoByIdAsync(id);
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
