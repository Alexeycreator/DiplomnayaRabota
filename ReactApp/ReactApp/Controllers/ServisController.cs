using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServisController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ServisController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Метод для отображения таблицы
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select id, name, rashirenie from dbo.Servis";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CalendarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConnection.Close();
                }
            }
            return new JsonResult(table);
        }

        //Метод вставки данных
        [HttpPost]
        public JsonResult Post(Servis serv)
        {
            string query = @"
                            insert into dbo.Servis
                            (name,rashirenie)
                            values
                            (
                            '" + serv.name + @"',
                            '" + serv.rashirenie + @"'
                            )
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CalendarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConnection.Close();
                }
            }
            return new JsonResult("Добавлено успешно");
        }

        //Метод обновления данных
        [HttpPut]
        public JsonResult Put(Servis serv)
        {
            string query = @"
                            update dbo.Servis set
                            name = '" + serv.name + @"',
                            rashirenie = '" + serv.rashirenie + @"'
                            where id = " + serv.id + @"
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CalendarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConnection.Close();
                }
            }
            return new JsonResult("Обновлено успешно");
        }

        //Метод удаления
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Servis
                            where id = " + id + @"
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CalendarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConnection.Close();
                }
            }
            return new JsonResult("Удалено успешно");
        }
    }
}
