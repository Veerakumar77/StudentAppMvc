using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentApp;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace StudentApp.Controllers
{
    public class SubjectController : Controller
    {
        private crudEntities db = new crudEntities();

        Uri baseAddress = new Uri("http://studentrestapi/api/CrudApi/");
        HttpClient client;
        public SubjectController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
         
        public ActionResult Subjects()
        {
            List<subject> subjectlist = new List<subject>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "subject").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                subjectlist = JsonConvert.DeserializeObject<List<subject>>(data);
            }
                return View(subjectlist);
        }

        
        public ActionResult Details(int id)
        {
            subject model = new subject();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "subject/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<subject>(data);
            }
             
            return View(model);
        }
 
        public ActionResult CreateSubject()
        {
            return View();
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubject(subject model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "subject" ,content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Subjects");
            }

            return View("CreateSubject",model);
        }

        
        public ActionResult EditSubject(int id)
        {
            subject s = new subject();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "subject/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                s = JsonConvert.DeserializeObject<subject>(data);
            }
            return View("EditSubject",s);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubject(subject model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "subject/" + model.SubjectId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Subjects");
            }
            return View("EditSubject",model);
        }

         
        public ActionResult Delete(int id)
        {
            subject s = new subject();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "subject/" + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                s = JsonConvert.DeserializeObject<subject>(data);
            }
            return View(s);
        }

        [HttpPost, ActionName ("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "subject/" + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Subjects");
            }
            return View("Delete");
        }

         

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
