using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWI_Form_Client.Models;
using SWI_Form_Client.Utility;
using System.Diagnostics;
using System.Text;


namespace SWI_Form_Client.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Form()
        {
            if (!await SessionHelper.checkLogin(HttpContext).ConfigureAwait(true))
            {
                return View("Views/Auth/Login.cshtml", new Login_View_Model
                {
                    loginReminder = true
                });
            }
            return View();
        }

        public async Task<IActionResult> SingleForm(string id)
        {
            if (!await SessionHelper.checkLogin(HttpContext).ConfigureAwait(true))
            {
                return View("Views/Auth/Login.cshtml", new Login_View_Model
                {
                    loginReminder = true
                });
            }
            try
            {
                var response = await HttpClientHelper.Get(null, "Login/GetForm?id=" + id,
                true, HttpContext.Session.GetString("AccessToken")).ConfigureAwait(true);

                Console.WriteLine(response);

                var model = JsonConvert.DeserializeObject<FormResponse>(response);

                if (model != null)
                {
                    return View(FormHelper.ParseToView(model.form));
                }
                else return RedirectToAction("Index", "Home");
            }
            catch (Exception exception)
            {
                Console.WriteLine("HomeController Forms " + exception.Message);
                return View("Error");
            }
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm(FormViewModels model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            var json = JsonConvert.SerializeObject(FormHelper.ParseToServer(model));
            try
            {
                var response = await HttpClientHelper.Post(json, "Login/SubmitForm", true, HttpContext.Session.GetString("AccessToken")).ConfigureAwait(true);

                if (response != null)
                {
                    if (response == String.Empty)
                    {
                        return View("Views/Auth/Login.cshtml", new Login_View_Model
                        {
                            loginReminder = true
                        });
                    }
                }

                var stuff = JsonConvert.DeserializeObject<FormResponse>(response);
                if (stuff != null)
                {
                    if (stuff.Status)
                    {
                        return View(FormHelper.ParseToView(stuff.form));
                    }
                    else
                    {
                        TempData["FormError"] = "Submission Failure";
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7037/Login/SubmitForm");


                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/data", content);
                if (response.IsSuccessStatusCode)
                {
                    return View("SubmitForm", true);
                }




                return View("Form", model);

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditForm(FormViewModels model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                Console.Write(errors);
            }
            var json = JsonConvert.SerializeObject(FormHelper.ParseToServer(model));
            try
            {
                var response = await HttpClientHelper.Post(json, "Login/EditForm", true, HttpContext.Session.GetString("AccessToken")).ConfigureAwait(true);
                var stuff = JsonConvert.DeserializeObject<FormResponse>(response);
                if (stuff != null)
                {
                    if (stuff.Status)
                    {
                        return View("SubmitForm");
                    }
                    else
                    {
                        TempData["FormError"] = "Submission Failure";
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7037/Login/EditForm");


                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/data", content);
                if (response.IsSuccessStatusCode)
                {
                    return View("SubmitForm", true);
                }




                return View("Form", model);

            }
            return View(model);
        }


        public async Task<IActionResult> Forms(int? page, int? pageLength, string? sortBy, string? filterBy)
        {
            //Enforce default values
            page ??= 1;
            pageLength ??= 1;
            sortBy ??= "none";
            filterBy ??= "none";

            if (page < 1)
            {
                page = 1;
            }

            if (pageLength < 5)
            {
                pageLength = 5;
            }

            try
            {
                var response = await HttpClientHelper.Get(null, "Login/GetFormList?page=" + page + "&pageLength=" + pageLength + "&sortBy=" + sortBy + "&filterBy=" + filterBy,
                    true, HttpContext.Session.GetString("AccessToken")).ConfigureAwait(true);

                if (response != null)
                {
                    if (response == String.Empty)
                    {
                        return View("Views/Auth/Login.cshtml", new Login_View_Model
                        {
                            loginReminder = true
                        });
                    }
                }

                var model = JsonConvert.DeserializeObject<FormListResponse>(response);

                return View(model);
            }
            catch (Exception exception)
            {
                Console.WriteLine("HomeController Forms " + exception.Message);
                return View("Error");
            }
        }

        public IActionResult Delete(string id)
        {
            if (id == null) return RedirectToAction("Forms", "Home");

            ViewBag.id = id; 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ActuallyDelete(string id)
        {
            if (id == null) return RedirectToAction("Forms", "Home");

            ViewBag.id = id; 
            try
            {
                var response = await HttpClientHelper.Get(null, "Login/DeleteForm?id=" + id,
                true, HttpContext.Session.GetString("AccessToken")).ConfigureAwait(true);

                var model = JsonConvert.DeserializeObject<Response>(response);

                return RedirectToAction("Forms", "Home");
            }
            catch (Exception exception)
            {
                Console.WriteLine("HomeController Forms " + exception.Message);
                return View("Error");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            //HttpContext.Session.Abandon();
            return RedirectToAction("Login", "Auth");
        }
    }
}