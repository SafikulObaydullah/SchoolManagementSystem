using MVCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class StudentsController : Controller
    {
        string baseUri = "https://localhost:44372/api/Students";
        private List<StudentVM> GetallStudents()
        {
            List<StudentVM> list = new List<StudentVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<StudentVM>>().Result;

                    }
                    else
                    {
                        ModelState.AddModelError("", "Retrieve failed");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            return list;

        }
        // GET: Students
        public ActionResult Index()
        {

            //List<StudentVM> list = new List<StudentVM>();
            //using (var client = new HttpClient())
            //{
            //    try
            //    {

            //        var result = client.GetAsync(baseUri).Result;
            //        if(result.IsSuccessStatusCode)
            //        {

            //            list = result.Content.ReadAsAsync<List<StudentVM>>().Result;
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Retrieve fail");
            //        }

            //    }
            //    catch(Exception ex)
            //    {
            //        ModelState.AddModelError("", ex.Message);
            //    }
            //    finally
            //    {

            //    }
            //}
            return View(GetallStudents());
        }
        public List<StudentVM> GetStudent()
        {
            List<StudentVM> list = new List<StudentVM>();
            list = GetallStudents();
            //list = list.Where(e => e.TypeID == 1).ToList();
            list = list.OrderBy(l => l.Name).ToList();
            return list;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.InstId = new SelectList(new InstituteInfosController().GetInstituteInfo(),
                "Id", "InstituteName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudentVM studentVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    var result = client.PostAsJsonAsync(baseUri, studentVM).Result;
                    if(result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            return View(studentVM);
        }
        public ActionResult Delete(int id)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage httpResponseMessage = client.DeleteAsync($"{baseUri}/{id}").Result;
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            StudentVM studentVM = null;
            using(var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if(result.IsSuccessStatusCode)
                    {
                        
                        studentVM = result.Content.ReadAsAsync<StudentVM>().Result;
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(studentVM);
        }
        [HttpPost]
        public ActionResult Edit(StudentVM studentVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                   
                    var result = client.PutAsJsonAsync(baseUri, studentVM).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update fail");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            return View(studentVM);
            
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            StudentVM studentVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if (result.IsSuccessStatusCode)
                    {

                        studentVM = result.Content.ReadAsAsync<StudentVM>().Result;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(studentVM);
        }

    }
}