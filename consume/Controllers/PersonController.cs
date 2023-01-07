using consume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace consume.Controllers
{
    public class PersonController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://img-api.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Get Method
                HttpResponseMessage response = await client.GetAsync("api/Person/");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    List<Person> allPerson = JsonConvert.DeserializeObject<List<Person>>(result);
                    return View(allPerson);
                }
                else
                {
                    return View();
                }
            }
        }





        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person, List<IFormFile> Image)
        {
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        person.Image = stream.ToArray();
                    }
                }
            }

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://img-api.azurewebsites.net/api/Person/";

                HttpResponseMessage response = await client.PostAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    TempData["AlertMsg"] = "Record has been succesfully saved";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://img-api.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Get Method
                HttpResponseMessage response = await client.GetAsync("api/Person/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Person person = JsonConvert.DeserializeObject<Person>(result);
                    return View(person);
                }
                else
                {
                    return View();
                }
            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(int Id, Person person, List<IFormFile> Image)
        {
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        person.Image = stream.ToArray();
                    }
                }
            }


            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://img-api.azurewebsites.net/api/Person/" + Id;

                HttpResponseMessage response = await client.PutAsync(url, data);
                //HttpResponseMessage response = await client.PatchAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    //string result = response.Content.ReadAsStringAsync().Result;
                    TempData["AlertMsg"] = " The record has been successfully saved";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }



        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://img-api.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Get Method
                HttpResponseMessage response = await client.GetAsync("api/Person/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Person person = JsonConvert.DeserializeObject<Person>(result);
                    return View(person);
                }
                else
                {
                    return View();
                }
            }
        }






        //[HttpPost]
        //For Direct Delete
        public async Task<IActionResult> Del(int Id)
        {
            using (var client = new HttpClient())
            {
                var url = "https://img-api.azurewebsites.net/api/Person/" + Id;

                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    //string result = response.Content.ReadAsStringAsync().Result;
                    TempData["AlertMsg"] = " The record has been succesfully deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }












        // GET: MovieDetails/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://img-api.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Get Method
                HttpResponseMessage response = await client.GetAsync("api/Person/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Person person = JsonConvert.DeserializeObject<Person>(result);
                    return View(person);
                }
                else
                {
                    return View();
                }
            }
        }

        // POST: MovieDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            using (var client = new HttpClient())
            {
                var url = "https://img-api.azurewebsites.net/api/Person/" + Id;

                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    //string result = response.Content.ReadAsStringAsync().Result;
                    TempData["AlertMsg"] = "The record has been succesfully deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }
    }
}
