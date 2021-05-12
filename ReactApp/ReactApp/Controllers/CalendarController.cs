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
    public class CalendarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CalendarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Метод для отображения таблицы
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select id, servis_id, login_cal, password_cal from dbo.Calendar";
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
        public JsonResult Post(Calendar cal)
        {
            string query = @"
                            insert into dbo.Calendar
                            (servis_id,login_cal,password_cal)
                            values
                            (
                            '" + cal.servis_id + @"',
                            '" + cal.login_cal + @"',
                            '" + cal.password_cal + @"'
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
        public JsonResult Put(Calendar cal)
        {
            string query = @"
                            update dbo.Calendar set
                            servis_id = '" + cal.servis_id + @"',
                            login_cal = '" + cal.login_cal + @"',
                            password_cal = '" + cal.password_cal + @"'
                            where id = " + cal.id + @"
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
                            delete from dbo.Calendar
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

        //Метод выведения названия сервисов
        [Route("GetAllServisNames")]
        public JsonResult GetAllServisNames()
        {
            string query = @"select name from dbo.Servis";
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
    }
}