using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySqlConnector;
using System.Data;
using WebApiApplication.Data;

namespace WebApiApplication.Controllers
{
    public class ValuesController : Controller
    {

        private UserDb _ctx;

        public ValuesController(UserDb ctx) 
        {
            _ctx = ctx;
        }

        
        [Route("Values/Numbers")]
        public ViewResult Sum([FromQuery] int first_number, [FromQuery] int second_number) // Суммирование 2 чисел через GET запрос и вывод экрана с результатом
        {
            
            int Sum = first_number + second_number;
            ViewBag.Sum = Sum;
            return View();
        }

        [Route("Registration")]
        public ViewResult RegistrationCompleate([FromQuery] string name, [FromQuery] string login, [FromQuery] string password) // Регистрация
        {
            User user = new User();

            var UserInDB = _ctx.Users.Where(u => u.email.Equals(login)).ToArray(); // Проверка, находится ли в базе данных пользователь с таким же логином
            
            if (UserInDB.Count() > 0)
                return View("~/Views/Values/RegistrationFailed.cshtml");

            user.name = name; // Запись данныхпользователя
            user.email = login;
            user.password = password;

            _ctx.Users.AddAsync(user); // Добавление пользователя в базу данных
            _ctx.SaveChanges();
            return View();
        }

        [Route("Login")]
        [HttpGet]
        public ViewResult LoginCompleate([FromQuery] string login, [FromQuery] string password) // Вход
        {
            
            var User = _ctx.Users.Where(u => u.email.Equals(login)).Where(u => u.password.Equals(password)).ToArray(); // Поиск пользователя в базе данных

            if (User.Count() > 0)
            {
                ViewBag.Name = User[0].name; 
                return View();
            }
            else
                return View("~/Views/Values/LoginFailed.cshtml");
        }

        public ViewResult TwoNumber()
        {
            return View(); // Форма для ввода чисел
        }

        public ViewResult Registration()
        {
            return View(); // Форма для регистрации
        }
    }
}
