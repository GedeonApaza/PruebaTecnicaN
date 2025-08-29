using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaN.DTOs;
using PruebaTecnicaN.Repositories;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaTecnicaN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSetsController : ControllerBase
    {
        private readonly IDatasetRepository _repository;

        public DataSetsController(IDatasetRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        //1
        // GET: api/<DataSetsController>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<DatasetsDto>>> GetDatasetsByUserId(int userId)
        {
            var datasets = await _repository.GetDatasetsByUserIdAsync(userId);

            return Ok(datasets);
        }
        //2
        [HttpPost]
        public async Task<IActionResult> CreateDatasetAsync([FromBody] DatasetsDto createDatasetDto)
        {
            try
            {
                var createdDataset = await _repository.CreateDatasetAsync(createDatasetDto);

                return Ok(new
                {
                    message = "DataSet creado exitosamente",
                    dataset = createdDataset
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado." });
            }
        }


        //3
        [HttpGet("procedure/{procedureId}")]
        public async Task<ActionResult<List<DatasetsDto>>> GetDatasetsByProcedureId(int procedureId)
        {
            try
            {
                if (procedureId <= 0)
                {
                    return BadRequest(new { message = "El ProcedureID debe ser mayor a 0" });
                }

                var datasets = await _repository.GetDatasetsByProcedureIdAsync(procedureId);

                if (!datasets.Any())
                {
                    return NotFound(new
                    {
                        message = $"No se encontraron DataSets para el procedimiento {procedureId}",
                        procedureId = procedureId
                    });
                }

                return Ok(new
                {
                    procedureId = procedureId,
                    totalDatasets = datasets.Count,
                    datasets = datasets
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }
        //4
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProcedure([FromBody] ProceduresDto procedureDto)
        {
            try
            {
                var createdProcedure = await _repository.CreateProcedureAsync(procedureDto);
                return Ok(new
                {
                    message = "Procedure creado exitosamente",
                    procedure = createdProcedure
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
