using Microsoft.AspNetCore.Mvc;
using Parcial1_P4_Leanny.Models;
using Parcial1_P4_Leanny.Services;

namespace Parcial1_P4_Leanny.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : ControllerBase
    {
        private readonly NumbersService _numbersService;

        public NumberController(NumbersService numbersService)
        {
            _numbersService = numbersService;
        }

        [HttpGet]
        public async Task<string> Get(double numero)
        {
            double resultado = numero + numero;

            var record = new NumberRecord(
                0,
                DateTime.Now,
                numero,
                resultado
            );

            await _numbersService.SaveAsync(record);

            return $"El resultado de la suma es {resultado}";
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NumberRecord>> GetById(int id)
        {
            var record = await _numbersService.GetByIdAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpGet("historial")]
        public async Task<ActionResult<IEnumerable<NumberRecord>>> GetList()
        {
            var records = await _numbersService.GetListAsync();

            return Ok(records);
        }
    }
}