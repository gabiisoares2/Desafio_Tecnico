using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioTecnico.DAO.Dao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DesafioTecnico.Web.Controllers
{
    public class LoginController : Controller
    {
        #region [ Atributos ]

        private readonly IConfiguration configuration;

        #endregion

        #region [ Construtor ]
        public LoginController(IConfiguration config)
        {
            configuration = config;
        }
        #endregion

        #region [ Actions ]

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string senha)
        {
            LoginDAO loginDAO = new LoginDAO(configuration);
            var log = loginDAO.VerificaLoginSenha(email, senha);
            if (log != null)
            {
                HttpContext.Session.SetString("log", log.Id.ToString());
                return RedirectToAction("Index", "Home"); ; 
            }
            else
            {
                ViewBag.MsgErro = "Não foi possível efetuar seu login, verifique os dados e tente novamente";
                return View();
            }
        }

        #endregion
    }
}
