using Newtonsoft.Json;

namespace SWI_Form_Client.Utility
{
    public static class SessionHelper
    {
        /// <summary>
        /// Checks the user's JWT against the API to check if they are still logged in.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<bool> checkLogin(HttpContext context)
        {
            if (context.Session != null)
            {
                var response = await HttpClientHelper.Get(null, "Login/ValidateLogin", true, context.Session.GetString("AccessToken"));
                if (response != null)
                {
                    if (response != String.Empty)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static async Task<bool> checkAdminLogin(HttpContext context)
        {
            if (context.Session != null)
            {
                var response = await HttpClientHelper.Get(null, "Login/ValidateLogin", true, context.Session.GetString("AccessToken"));
                if (response != null)
                {
                    if (response != String.Empty)
                    {
                        bool output = (bool)JsonConvert.DeserializeObject(response, typeof(bool));
                        return output;
                    }
                }
            }
            return false;
        }
    }
}
