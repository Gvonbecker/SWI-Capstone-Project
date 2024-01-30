using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWI_Form_Client.Models;
using SWI_Form_Client.Utility;
using System.Text;


namespace SWI_Form_Client.Controllers
{
    public class AdminController : Controller
    {

        public async Task<IActionResult> RegisterUser()
        {
            if (!await SessionHelper.checkAdminLogin(HttpContext).ConfigureAwait(true))
            {
                return View("Views/Auth/Login.cshtml", new Login_View_Model
                {
                    loginReminder = true
                });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(Login model)
        {
            if (ModelState.IsValid)
            {
                model.password = EncryptionHelper.HashPassword(model, model.password);

                var json = JsonConvert.SerializeObject(model);

                try
                {
                    var response = await HttpClientHelper.Post(json, "Login/Register", true, HttpContext.Session.GetString("AccessToken")).ConfigureAwait(true);
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
                    var stuff = JsonConvert.DeserializeObject<Response>(response);
                    if (stuff != null)
                    {

                        if (stuff.Status)
                        {
                            return RedirectToAction("Form", "Home");

                        }

                        else
                        {
                            TempData["LoginError"] = "Registration failed (Does a user with this name already exist?)";
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return RedirectToAction("RegisterUser", "Admin");
                        }
                    }
                    else
                    {
                        TempData["LoginError"] = "Registration failed (Does a user with this name already exist?)";
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return RedirectToAction("RegisterUser", "Admin");
                    }

                }


                catch (Exception ex)
                {
                    return View("Error");
                }
                // Convert the data to a JSON string


                // Send the HTTP POST request






            }
            return View(model);
        }
    }
}
