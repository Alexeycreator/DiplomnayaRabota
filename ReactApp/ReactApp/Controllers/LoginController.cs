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
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Метод для отображения таблицы
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select id, namelogin, password, midname, firstname, calendar_id from dbo.login";
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
        public JsonResult Post(Login log)
        {
            string query = @"
                            insert into dbo.Login
                            (nameLogin,password,midname,firstname,calendar_id)
                            values
                            (
                            '" + log.nameLogin + @"',
                            '" + log.password + @"',
                            '" + log.midname + @"',
                            '" + log.firstname + @"',
                            '" + log.calendar_id + @"'
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
        public JsonResult Put(Login log)
        {
            string query = @"
                            update dbo.Login set
                            nameLogin = '" + log.nameLogin + @"',
                            password = '" + log.password + @"',
                            midname = '" + log.midname + @"',
                            firstname = '" + log.firstname + @"',
                            calendar_id = '" + log.calendar_id + @"'
                            where id = " + log.id + @"
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
                            delete from dbo.Login
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