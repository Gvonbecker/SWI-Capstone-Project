using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWI_Form_Client.Models;
using SWI_Form_Client.Utility;
using System.Security.Claims;

namespace SWI_Form_Client.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(Login_View_Model model)
        {
            if (ModelState.IsValid)
            {
                var data = new
                {
                    Username = model.username,
                    Password = model.Password
                };

                var json = JsonConvert.SerializeObject(data);


                try
                {
                    var response = await HttpClientHelper.Post(json, "Login/Login", false, null).ConfigureAwait(true);
                    var stuff = JsonConvert.DeserializeObject<LoginResponse>(response);
                    if (stuff != null)
                    {

                        if (stuff.Status)
                        {



                            // Read the response JSON and save the access token to the session


                            //var token = JsonConvert.DeserializeObject<string>(responseJson);

                            HttpContext.Session.SetString("AccessToken", stuff.authToken);
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,model.username)
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            return RedirectToAction("Forms", "Home");
                        }

                        else
                        {
                            TempData["LoginError"] = "Invalid username or password";
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return RedirectToAction("Login", "Auth");
                        }
                    }
                    else
                    {
                        TempData["LoginError"] = "Invalid username or password";
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return RedirectToAction("Login", "Auth");
                    }

                    return View(model);

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




