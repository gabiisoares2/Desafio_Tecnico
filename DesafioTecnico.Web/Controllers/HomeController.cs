using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DesafioTecnico.Web.Models;
using DesafioTecnico.DAO.Model;
using System.Reflection.Metadata.Ecma335;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using DesafioTecnico.DAO.Dao;
using Microsoft.Extensions.Configuration;

namespace DesafioTecnico.Web.Controllers
{
    public class HomeController : Controller
    {
        #region [ Atributos ]

        public const string sessionLog = "log";
        public CalculoDividas CalculoDividas { get; set; }

        private readonly IConfiguration configuration;

        #endregion

        #region [ Construtor ]
        public HomeController(IConfiguration config)
        {
            configuration = config;
        }
        #endregion

        #region [ Actions ]

        public IActionResult Index()
        {
            var idLog = HttpContext.Session.GetString(sessionLog);
            if (idLog != null)
            {
                ViewBag.ListaDividas = new DividaAtivaDAO(configuration).RecuperarDividaPorLogin(Convert.ToInt32(idLog));

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult<CalculoDividas> Resultado(string qtde, string porcentagem, string idDivida)
        {
            return View(CalcularJuros(qtde, porcentagem, idDivida));
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Remove(sessionLog);
            var teste = HttpContext.Session.GetString(sessionLog);
            if (teste == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region [ Métodos ]

        public CalculoDividas CalcularJuros(string qtde, string porcentagem, string idDivida)
        {
            try
            {
                DividaAtiva dividaAtiva = new DividaAtiva();

                dividaAtiva = new DividaAtivaDAO(configuration).RecuperarDividaPorId(Convert.ToInt32(idDivida));

                if (dividaAtiva == null)
                {
                    dividaAtiva.Descricao = "Divida 1";
                    dividaAtiva.DataVencimento = DateTime.Now.AddDays(-10);
                    dividaAtiva.Contato = "(14) 9 8111-7159";
                }
                CalculoDividas calculoDividas = new CalculoDividas();
                calculoDividas.Descricao = dividaAtiva.Descricao;
                calculoDividas.DataVencimento = dividaAtiva.DataVencimento;
                calculoDividas.ValorOriginal = Convert.ToDouble(dividaAtiva.ValorDivida);
                calculoDividas.DiasAtraso = (DateTime.Now.Date - calculoDividas.DataVencimento.Date).TotalDays;
                calculoDividas.ValorFinal = calculoDividas.ValorOriginal * (1 + (0.2 / 100) * calculoDividas.DiasAtraso);
                var comissao = calculoDividas.ValorFinal * (Convert.ToDouble(porcentagem) / 100);
                calculoDividas.ValorJuros = calculoDividas.ValorOriginal * (0.2 / 100) * calculoDividas.DiasAtraso;
                var valorParcelas = Math.Round((calculoDividas.ValorFinal / Convert.ToDouble(qtde)), 2);

                for (int i = 0; i < Convert.ToDouble(qtde); i++)
                {
                    calculoDividas.ListaParcelas.Add(new ValorFinalParcelas
                    {
                        NumeroParcela = i + 1,
                        ValorParcela = valorParcelas,
                        DataVencimentoParcela = DateTime.Now.AddMonths(i + 1)
                    });
                }

                calculoDividas.Contato = dividaAtiva.Contato;

                return calculoDividas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
