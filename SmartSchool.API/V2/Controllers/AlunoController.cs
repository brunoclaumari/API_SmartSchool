using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.V1.DTOS;
using SmartSchool.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.V2.Controllers
{
    /// <summary>
    /// Controller da entidade Aluno versão 2.0
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
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
        public IActionResult Get()
        {            
            var listaAlunos = _repo.GetAllAlunos(true);            

            var enumerableRetorno = _mapper.Map<IEnumerable<AlunoDTO>>(listaAlunos);

            return Ok(enumerableRetorno);
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
            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return Ok(alunoDTO);
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
