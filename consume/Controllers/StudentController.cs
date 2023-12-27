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
    public class std
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
        public byte[] Image { get; set; }
    }
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
                    List<std> stdobject = JsonConvert.DeserializeObject<List<std>>(result);
                    foreach (var st in stdobject)
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





        public static IFormFile BytesArrayToIFormFile(byte[] BytesPhoto)
        {
            var stream = new MemoryStream(BytesPhoto);
            IFormFile ImageIFormFile = new FormFile(stream, 0, (BytesPhoto).Length, "name", "fileName");
            return ImageIFormFile;
        }
    }
}
