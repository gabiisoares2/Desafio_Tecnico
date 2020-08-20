using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioTecnico.DAO.Model
{
    public class DividaAtiva
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal ValorDivida { get; set; }
        public DateTime DataVencimento { get; set; }
        public int IdLogin { get; set; }
        public string Contato { get; set; }
    }
}
