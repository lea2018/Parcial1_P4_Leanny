using Microsoft.AspNetCore.Mvc;

namespace ApiPlanetas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : ControllerBase
    {
        [HttpGet]
        public double Get(double numero)
        {
            return numero + numero;
        }
    }
}
