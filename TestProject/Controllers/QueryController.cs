using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class QueryController : Controller
    {
        // GET: User with age 24
        public ActionResult GetAge()
        {
            var dbContext = new ApplicationDbContext();
            var ageList = (from user in dbContext.Users
                           select new LinqViewModel
                           {
                               UserName = user.UserName,
                               Email = user.Email,
                               City = user.City,
                               Age = user.Age,
                               Occupation = user.Occupation
                           }).Where(user => user.Age > 20).OrderByDescending(b => b.Age).ToList();

            return View(ageList);
        }

        public JsonResult ListString()
        {
            List<Person> names = new List<Person>
            {
                new Person { First = "John", Last = "Doe", City = "LA", ID=1},
                new Person {First = "Jane", Last = "Doe", City = "LA"},
                new Person {First = "Anna", Last = "Smith", City = "Seattle"},
                new Person {First = "Lisa", Last = "Smith", City = "Seattle"},
                new Person {First = "Carrie", Last = "Wilder", City = "Wisconsin"},
                new Person {First = "Laura", Last = "Wilder", City = "Seattle"}
            };

            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher { First = "Karen", Last="Ingalls", City = "LA"},
                new Teacher { First = "Lisa", Last="Jona", City = "LA"},
                new Teacher { First = "Lyra", Last="Goji", City = "Seattle"},
                new Teacher { First = "Gerda", Last="Polsa", City = "Seattle"},
                new Teacher { First = "Dolja", Last="Frank", City = "Wisconsin"},
                new Teacher { First = "Laria", Last="Grady", City = "LA"}
            };

            var query = (from name in names
                         where name.City == "Seattle" && name.First.StartsWith("L")
                         select name.First)
                        .Concat(from teacher in teachers
                                where teacher.City == "Seattle"
                                select teacher.First);
                        
                        
                      

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JoinQuery()
        {
            var dbContext = new ApplicationDbContext();

            var testScores = (from user in dbContext.TestProtocols
                              join UserName in dbContext.Users
                              on user.UserName equals UserName.UserName
                              select new TestProtocolUserViewModel
                              {
                                  UserId = user.UserId,
                                  UserName = user.UserName,
                                  TestId = user.TestId,
                                  TestDate = user.TestDate,
                                  TestScore = user.TestScore,
                                  City = UserName.City
                              }).Where(u => u.TestScore >= 27).GroupBy(m => m.UserName).ToList();
       

            return Json(testScores, JsonRequestBehavior.AllowGet);
        }
    }
}