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
        [HttpPost]
        public ActionResult Index(string Month,string Year,string Amount)
        {
            int Amt;
            List<Expens> Exp_list = new List<Expens>();
            client.BaseAddress = new Uri("http://localhost:64815/api/Expenseapi");
            var response = client.GetAsync("ExpenseApi");
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Expens>>();
                display.Wait();
                Exp_list = display.Result;
            }
            if (Amount != null)
            {
                Amt = int.Parse(Amount);
                Exp_list = Exp_list.Where(m => m.Amount > Amt).ToList();
            }
            else
            {
                if (Month != "0" && Year !=null)
                {
                    Exp_list = Exp_list.Where(m => m.Month == Month && m.year == Year).ToList();
                }
                else if (Month != "0")
                {
                    Exp_list = Exp_list.Where(m => m.Month == Month).ToList();
                }
                else if (Year != null)
                {
                    Exp_list = Exp_list.Where(m => m.year == Year).ToList();
                }
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
            ModelState.AddModelError(string.Empty, "Data already present please modify it");
            return View(e);
        }
        public ActionResult Details(int id)
        {
            Expens e=null;
            client.BaseAddress = new Uri("http://localhost:64815/api/Expenseapi");
            var response = client.GetAsync("ExpenseApi?id=" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Expens>();
                display.Wait();
                e = display.Result;
            }
          return View(e);
        }
        public ActionResult Edit(int id)
        {
            Expens e = null;
            client.BaseAddress = new Uri("http://localhost:64815/api/Expenseapi");
            var response = client.GetAsync("ExpenseApi?id=" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Expens>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }
        [HttpPost]
        public ActionResult Edit(Expens e)
        {
            client.BaseAddress = new Uri("http://localhost:64815/api/Expenseapi");
            var response = client.PutAsJsonAsync<Expens>("ExpenseApi", e);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View("Edit");
        }
         public ActionResult Delete(int id)
        {
            Expens e = null;
            client.BaseAddress = new Uri("http://localhost:64815/api/Expenseapi");
            var response = client.GetAsync("ExpenseApi?id=" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Expens>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            client.BaseAddress = new Uri("http://localhost:64815/api/Expenseapi");
            var response = client.DeleteAsync("ExpenseApi/"+id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View("Delete");
        }
    }

}