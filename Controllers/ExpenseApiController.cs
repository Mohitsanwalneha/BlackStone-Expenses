using BlackStone_Expenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlackStone_Expenses.Controllers
{
    public class ExpenseApiController : ApiController
    {
        ExpensesEntitiesDb db = new ExpensesEntitiesDb();
        [HttpGet]
       public IHttpActionResult Get()
        {
            List<Expens> list = db.Expenses.ToList();
            return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult Put(Expens e)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            db.Expenses.Add(e);
            db.SaveChanges();
            return Ok(); ;
        }
    }
}
