using ApiSaSTokenStorageTables.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSaSTokenStorageTables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableTokenController : ControllerBase
    {
        private ServiceSaSToken service;

        public TableTokenController(ServiceSaSToken service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("[action]/{curso}")]
        public ActionResult<string> GenerateToken(string curso)
        {
            string token =
                this.service.GenerateSaSToken(curso);
            //PODEMOS PERSONALIZAR EL JSON QUE DEVOLVEMOS
            // { numeroregistros: 5, datos: List<T> }
            //return Ok(
            //    new
            //    {
            //        numeroregistros= 5,
            //        datos = new List<int>()
            //    });
            //{ token: TOKENVALUE }
            return Ok(new
            {
                token = token
            });
        }

    }
}
