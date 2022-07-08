using BlackStone_Expenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BlackStone_Expenses.Controllers
{
    public class ExpenseMvcController : Controller
    {
        HttpClient client =new HttpClient();
        // GET: ExpenseMvc
        public ActionResult Index()
        {
            List<Expens> Exp_list = new List<Expens>();
            client.BaseAddress = new Uri("http://localhost:64815/api/Expenseapi");
            var response =client.GetAsync("ExpenseApi");
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Expens>>();
                display.Wait();
                Exp_list = display.Result;
            }

            return View(Exp_list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
            [HttpPost]
        public ActionResult Create( Expens e)
        {
            client.BaseAddress = new Uri("http://localhost:64815/api/Expenseapi");
            var response = client.PostAsJsonAsync<Expens>("ExpenseApi",e);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(e);
        }
    }
}