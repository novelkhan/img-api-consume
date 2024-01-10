using consume.DTOs.Student;
using consume.Models;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace consume.Controllers
{  
    public class StudentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://img.somee.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Get Method
                HttpResponseMessage response = await client.GetAsync("api/Student/");
                if (response.IsSuccessStatusCode)
                {
                    List<StudentDTO> students = new List<StudentDTO>();
                    var result = await response.Content.ReadAsStringAsync();
                    List<std> stdObject = JsonConvert.DeserializeObject<List<std>>(result);
                    foreach (var st in stdObject)
                    {
                        students.Add(new StudentDTO
                        {
                            Id = st.Id,
                            Name = st.Name,
                            Roll = st.Roll,
                            Image = BytesArrayToIFormFile(st.Image)
                        });
                    }
                    return View(students);
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
        public async Task<IActionResult> Add(StudentDTO student)
        {
            using (var client = new HttpClient())
            {
                var url = "https://img.somee.com/api/Student/";

                var multipartContent = new MultipartFormDataContent();

                multipartContent.Add(new StringContent(student.Name), "Name");
                //multipartContent.Add(new StringContent(JsonConvert.SerializeObject(student.Roll)), "Roll");
                multipartContent.Add(new StringContent(student.Roll), "Roll");
                multipartContent.Add(new StreamContent(student.Image.OpenReadStream()), "Image", student.Image.FileName);
                var response = await client.PostAsync(url, multipartContent);




                //HttpResponseMessage response = await client.PostAsync(url, student);
                if (response.IsSuccessStatusCode)
                {
                    //string result = response.Content.ReadAsStringAsync().Result;
                    //TempData["AlertMsg"] = "Record has been succesfully saved";
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
                client.BaseAddress = new Uri("https://img.somee.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Get Method
                HttpResponseMessage response = await client.GetAsync("api/Student/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    std deserializedStudent = JsonConvert.DeserializeObject<std>(result);
                    StudentDTO desiredStudent = new StudentDTO()
                    {
                        Id = deserializedStudent.Id,
                        Name = deserializedStudent.Name,
                        Roll = deserializedStudent.Roll,
                        Image = BytesArrayToIFormFile(deserializedStudent.Image)
                    };
                    return View(desiredStudent);
                }
                else
                {
                    return View();
                }
            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(StudentDTO student)
        {
            using (var client = new HttpClient())
            {
                var url = "https://img.somee.com/api/Student/" + student.Id;

                var multipartContent = new MultipartFormDataContent();


                multipartContent.Add(new StringContent((student.Id).ToString()), "Id");         // Need to pass Student Id here
                multipartContent.Add(new StringContent(student.Name), "Name");
                multipartContent.Add(new StringContent(student.Roll), "Roll");
                if (student.Image != null)
                {
                    multipartContent.Add(new StreamContent(student.Image.OpenReadStream()), "Image", student.Image.FileName);
                }

                //HttpResponseMessage response = await client.PutAsync(url, multipartContent);
                HttpResponseMessage response = await client.PutAsync(url, multipartContent);
                if (response.IsSuccessStatusCode)
                {
                    //string result = response.Content.ReadAsStringAsync().Result;
                    //TempData["AlertMsg"] = " The record has been successfully saved";
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
                client.BaseAddress = new Uri("https://img.somee.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Get Method
                HttpResponseMessage response = await client.GetAsync("api/Student/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    std deserializedStudent = JsonConvert.DeserializeObject<std>(result);
                    StudentDTO desiredStudent = new StudentDTO()
                    {
                        Id = deserializedStudent.Id,
                        Name = deserializedStudent.Name,
                        Roll = deserializedStudent.Roll,
                        Image = BytesArrayToIFormFile(deserializedStudent.Image)
                    };
                    return View(desiredStudent);
                }
                else
                {
                    return View();
                }
            }
        }






        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://img.somee.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Get Method
                HttpResponseMessage response = await client.GetAsync("api/Student/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    std deserializedStudent = JsonConvert.DeserializeObject<std>(result);
                    StudentDTO desiredStudent = new StudentDTO()
                    {
                        Id = deserializedStudent.Id,
                        Name = deserializedStudent.Name,
                        Roll = deserializedStudent.Roll,
                        Image = BytesArrayToIFormFile(deserializedStudent.Image)
                    };
                    return View(desiredStudent);
                }
                else
                {
                    return View();
                }
            }
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            using (var client = new HttpClient())
            {
                var url = "https://img.somee.com/api/Student/" + Id;

                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    //string result = response.Content.ReadAsStringAsync().Result;
                    //TempData["AlertMsg"] = "The record has been succesfully deleted";
                    return RedirectToAction("Index");
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
                var url = "https://img.somee.com/api/Student/" + Id;

                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    //string result = response.Content.ReadAsStringAsync().Result;
                    //TempData["AlertMsg"] = " The record has been succesfully deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }















        public static IFormFile BytesArrayToIFormFile(byte[] BytesPhoto)
        {
            var stream = new MemoryStream(BytesPhoto);
            IFormFile ImageIFormFile = new FormFile(stream, 0, (BytesPhoto).Length, "name", "fileName");
            return ImageIFormFile;
        }

        public class std
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Roll { get; set; }
            public byte[] Image { get; set; }
        }
    }
}
