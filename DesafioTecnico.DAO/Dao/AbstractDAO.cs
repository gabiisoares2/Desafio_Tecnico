using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioTecnico.DAO.Data
{
    public class AbstractDAO
    {
        private readonly IConfiguration _config;
        protected string ConnectionString => _config.GetConnectionString("DbConnection");

        protected AbstractDAO(IConfiguration config)
        {
            _config = config;
        }
    }
}
