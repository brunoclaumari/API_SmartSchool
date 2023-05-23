using AutoMapper;
using SmartSchool.API.DTOS;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public class SmartSchoolProfile : Profile
    {

        //private MapperConfiguration _configuration;

        public SmartSchoolProfile()
        {
            InicializaMapper();
        }

        private void InicializaMapper()
        {
            #region Map Aluno
            CreateMap<Aluno, AlunoDTO>()
                .ForMember(
                    dest => dest.Nome,
                    map => map.MapFrom(orig => $"{orig.Nome} {orig.Sobrenome}"))
                .ForMember(
                    dest2 => dest2.Idade, map =>
                    map.MapFrom(orig2 => orig2.DataNasc.GetIdadeAtual()));

            CreateMap<AlunoDTO, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDTO>().ReverseMap();
            #endregion

            #region Map Professor
            CreateMap<Professor, ProfessorDTO>()
                .ForMember(
                    dest => dest.Nome,
                    map => map.MapFrom(orig => $"{orig.Nome} {orig.Sobrenome}"));

            CreateMap<ProfessorDTO, Professor>();
            CreateMap<Professor, ProfessorRegistrarDTO>().ReverseMap();

            #endregion

            //CreateMap<AlunoDTO, Aluno>()
            //    .ForMember(
            //        ent => ent.Nome,
            //        map => map.MapFrom(dto => RetornaNomeModel(dto.Nome)))
            //    .ForMember(
            //        ent2 => ent2.Sobrenome,
            //        map2 => map2.MapFrom(dto2 => RetornaSobreNomeModel(dto2.Nome)))
            //    ;
        }

        private string RetornaNomeModel(string nomeDto)
        {
            var vetor = nomeDto.Split(" ");
            string retorno = vetor[0];

            return retorno.Trim();
        }

        private string RetornaSobreNomeModel(string nomeDto)
        {
            string retorno = string.Empty;
            var vetor = nomeDto.Split(" ");
            int index = 0;
            while (index < vetor.Length && index != 0)
            {
                retorno += vetor[index];
                index++;
            }

            return retorno.Trim();
        }

    }
}
