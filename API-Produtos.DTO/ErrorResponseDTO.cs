using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Produtos.DTO
{
    public class ErrorResponseDTO
    {
        public int status { get; set; }
        public string message { get; set; }
        public string info { get; set; }
    }
}
