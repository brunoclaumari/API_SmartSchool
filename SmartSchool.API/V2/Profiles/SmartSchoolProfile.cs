using AutoMapper;
using SmartSchool.API.V2.DTOS;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSchool.API.Helpers;

namespace SmartSchool.API.V2.Profiles
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
