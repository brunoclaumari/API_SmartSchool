using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Método tipo Extension methods para datetime. Retorna a idade
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetIdadeAtual(this DateTime dateTime)
        {
            var dataAtual = DateTime.UtcNow;
            
            int idade = new DateTime(DateTime.UtcNow.Subtract(dateTime).Ticks).Year;
            if (dataAtual.AddDays(9) < dateTime.AddYears(idade))
                idade--;

            return idade;
        }

    }
}
