using Dapper;
using Dapper.Contrib.Extensions;
using DesafioTecnico.DAO.Data;
using DesafioTecnico.DAO.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DesafioTecnico.DAO.Dao
{
    public class LoginDAO : AbstractDAO
    {
        public LoginDAO(IConfiguration config) : base(config)
        {
        }

        public Login VerificaLoginSenha(string email, string senha)
        {
            StringBuilder sql = new StringBuilder();

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    conn.Open();

                    sql.AppendLine("SELECT ID FROM LOGIN WHERE EMAIL = '" + email + "' AND SENHA = '" + senha + "'");

                    var resultado = conn.Query<Login>(sql.ToString()).FirstOrDefault();

                    if (resultado != null)
                    {
                        conn.Close();
                        return resultado;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
