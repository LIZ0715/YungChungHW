using Microsoft.AspNetCore.Mvc;
using Yungching.Rehouse.Web.Abstract;

namespace Yungching.Rehouse.Web.Controllers.API
{
    public class HomeApiController:BaseApiController
    {

        [HttpGet]
        public void  Get()
        {
            try
            {
                Console.WriteLine("Good");
            }
            catch
            {
                throw;
            }

        }


    }
}
