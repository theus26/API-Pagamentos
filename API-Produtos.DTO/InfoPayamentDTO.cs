using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Produtos.DTO
{
    public class InfoPayamentDTO
    {

            public float valor { get; set; }
            public Card cartao { get; set; }
    }

        public class Card
        {
            public string titular { get; set; }
            public string numero { get; set; }
            public string data_expiracao { get; set; }
            public string bandeira { get; set; }
            public string cvv { get; set; }
        }

}

