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
    public class StudentController : Controller
    {
        private crudEntities db = new crudEntities();

        Uri baseAddress = new Uri("http://studentrestapi/api/CrudApi/");
        HttpClient client;
        public StudentController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public JsonResult IsExist(string StudentName)
        {
            System.Threading.Thread.Sleep(200);
            if (db.students.Any(x => x.StudentName == StudentName))
           {
               return Json(1);
           }
           else
           {
               return Json(0);
           }
        }

        public ActionResult Students()
        {
            List<student> studentlist = new List<student>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "student").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                studentlist = JsonConvert.DeserializeObject<List<student>>(data);
            }
                return View(studentlist);
        }

        
        public ActionResult Details(int id)
        {
            student model = new student();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "student/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<student>(data);
            }
             
            return View(model);
        }
 
        public ActionResult CreateStudent()
        {
            ViewBag.SubjectId = new SelectList(db.subjects, "SubjectId", "Subjects");
            return View();
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(student model)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "student", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Students");
                }
                ViewBag.SubjectId = new SelectList(db.subjects, "SubjectId", "Subjects", model.SubjectId);
                return View("CreateStudent", model);
            }
            else
            {
                ViewBag.SubjectId = new SelectList(db.subjects, "SubjectId", "Subjects", model.SubjectId);
                return View("CreateStudent", model);
            }
            
        }

        
        public ActionResult EditStudent(int id)
        {
            
            student s = new student();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "student/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                s = JsonConvert.DeserializeObject<student>(data);
            }
            ViewBag.SubjectId = new SelectList(db.subjects, "SubjectId", "Subjects",s.SubjectId);
            return View(s);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(student model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "student/" + model.RegNo, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Students");
            }
            ViewBag.SubjectId = new SelectList(db.subjects, "SubjectId", "Subjects", model.SubjectId);
            return View("EditStudent",model);
        }


        public ActionResult Delete(int id)
        {
            student s = new student();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "student/" + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                s = JsonConvert.DeserializeObject<student>(data);
            }
            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "student/" + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Students");
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
