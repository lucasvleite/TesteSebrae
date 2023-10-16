using Microsoft.AspNetCore.Mvc;
using TesteSebrae.Servicos.Interfaces;
using TesteSebrae.Servicos.Util;
using TesteSebrae.WebApi.ViewModels;

namespace TesteSebrae.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViaCepController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRequisicaoViaCep<ViaCepResponse> _requisicaoViaCep;

        public ViaCepController(ILogger<ViaCepController> logger,
            IRequisicaoViaCep<ViaCepResponse> requisicaoViaCep)
        {
            _logger = logger;
            _requisicaoViaCep = requisicaoViaCep;
        }

        /// <summary>
        /// Busca informações de um cep
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        [HttpGet("{cep}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ViaCepResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync(string cep)
        {
            try
            {
                if (!Validadores.ValidaCep(cep))
                {
                    return BadRequest();
                }

                var resultado = await _requisicaoViaCep.ChamaViaCep(cep);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro api ViaCep.");
                return Problem("O serviço está com um problema no momento.");
            }
        }
    }
}
