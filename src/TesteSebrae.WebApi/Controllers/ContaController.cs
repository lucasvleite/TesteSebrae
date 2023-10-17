using Microsoft.AspNetCore.Mvc;
using TesteSebrae.Servicos.Interfaces;
using TesteSebrae.WebApi.ViewModels;

namespace TesteSebrae.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IContaServico _servico;

        public ContaController(IContaServico servico,
            ILogger<ContaController> logger)
        {
            _servico = servico;
            _logger = logger;
        }

        /// <summary>
        /// Busca Contas por paginação
        /// </summary>
        /// <param name="quandiadeIgnorar"></param>
        /// <param name="quantidadePegar"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ContaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> BuscaTodosPaginado(int quandiadeIgnorar = 0, int quantidadePegar = 10)
        {
            try
            {
                var resultado = await _servico.BuscaTodosPaginado(quandiadeIgnorar, quantidadePegar);
                var contas = resultado.Select(c => (ContaResponse)c);
                return Ok(contas);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro api {nameof(BuscaTodosPaginado)}.");
                return Problem("O serviço está com um problema no momento.");
            }
        }

        /// <summary>
        /// Busca Contas pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ContaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Procura(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest();
                }

                var resultado = await _servico.ProcuraPeloId(id);
                if (resultado == null)
                {
                    return NoContent();
                }

                return Ok((ContaResponse)resultado);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro api {nameof(Procura)}.");
                return Problem("O serviço está com um problema no momento.");
            }
        }

        /// <summary>
        /// Insere uma nova Conta
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ContaResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Adiciona([FromBody] ContaRequest conta)
        {
            try
            {
                if (conta == null)
                {
                    return BadRequest();
                }

                ContaResponse resultado = await _servico.Adiciona(conta);
                return CreatedAtAction(nameof(Adiciona), resultado);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro api {nameof(Adiciona)}.");
                return Problem("O serviço está com um problema no momento.");
            }
        }

        /// <summary>
        /// Atualiza Conta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="conta"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ContaResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Atualiza(Guid id, [FromBody] ContaRequest conta)
        {
            try
            {
                if (conta == null || id == Guid.Empty)
                {
                    return BadRequest();
                }

                await _servico.Atualiza(id, conta);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro api {nameof(Atualiza)}.");
                return Problem("O serviço está com um problema no momento.");
            }
        }

        /// <summary>
        /// Deleta Conta pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ContaResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Deleta(Guid id)
        {
            try
            {
                if (await _servico.Deleta(id))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro api {nameof(Deleta)}.");
                return Problem("O serviço está com um problema no momento.");
            }
        }
    }
}
