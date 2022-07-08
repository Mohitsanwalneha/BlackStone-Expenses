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
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var Ex = db.Expenses.Where(m => m.id == id).FirstOrDefault();

            return Ok(Ex);
        }
        [HttpPost]
        public IHttpActionResult ADD(Expens e)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            db.Expenses.Add(e);
            db.SaveChanges();
            return Ok(); ;
        }
        [HttpPut]
        public IHttpActionResult Update(Expens e)
        {
            db.Entry(e).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok();  
        }

    }
}
