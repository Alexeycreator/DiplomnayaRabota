using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ReactApp.Models;

namespace ReactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SobitieController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SobitieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Метод для отображения таблицы
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select id, calendar_id, name, opisanie, data from dbo.Sobitie";
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
        public JsonResult Post(Sobitie sob)
        {
            string query = @"
                            insert into dbo.Sobitie
                            (name,opisanie,calendar_id,data)
                            values
                            (
                            '" + sob.name + @"',
                            '" + sob.opisanie + @"',
                            '" + sob.calendar_id + @"',
                            '" + sob.data + @"'
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
        public JsonResult Put(Sobitie sob)
        {
            string query = @"
                            update dbo.Sobitie set
                            name = '" + sob.name + @"',
                            opisanie = '" + sob.opisanie + @"',
                            calendar_id = '" + sob.calendar_id + @"',
                            data = '" + sob.data + @"'
                            where id = " + sob.id + @"
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
                            delete from dbo.Sobitie
                            where id = "+ id +@"
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