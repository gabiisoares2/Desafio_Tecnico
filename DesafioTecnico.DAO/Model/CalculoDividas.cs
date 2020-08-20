using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DesafioTecnico.DAO.Model
{
    public class CalculoDividas
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public int QtdeParcelas { get; set; }
        public double ValorOriginal { get; set; }
        public double DiasAtraso { get; set; }
        public double ValorJuros { get; set; }
        public double ValorFinal { get; set; }
        public List<ValorFinalParcelas> ListaParcelas { get; set; } = new List<ValorFinalParcelas>();
        public string Contato { get; set; }
    }
}
