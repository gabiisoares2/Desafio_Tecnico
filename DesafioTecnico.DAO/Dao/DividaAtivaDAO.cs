using Dapper;
using DesafioTecnico.DAO.Data;
using DesafioTecnico.DAO.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DesafioTecnico.DAO.Dao
{
    public class DividaAtivaDAO : AbstractDAO
    {
        public DividaAtivaDAO(IConfiguration configuration) : base(configuration) { }

        public List<DividaAtiva> RecuperarDividaPorLogin(int Id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    sql.AppendLine("SELECT ID, DESCRICAO FROM DividasAtivas WHERE IdLogin = " + Id + "");

                    var resultado = conn.Query<DividaAtiva>(sql.ToString()).ToList();
                    
                    if(resultado.Count() > 0)
                    {
                        return resultado;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public DividaAtiva RecuperarDividaPorId(int Id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    sql.AppendLine("SELECT * FROM DividasAtivas WHERE Id = " + Id + "");

                    var resultado = conn.Query<DividaAtiva>(sql.ToString()).FirstOrDefault();

                    if (resultado != null)
                    {
                        return resultado;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}
