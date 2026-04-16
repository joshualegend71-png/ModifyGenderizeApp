using Microsoft.AspNetCore.Mvc;
using ModifyGenderizeApp.Services.Interface;

namespace ModifyGenderizeApp.Controllers
{
    [ApiController]
    [Route("api/classify")]
    public class ClassifyController : ControllerBase
    {
        private readonly IGenderizeService _service;

        public ClassifyController(IGenderizeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? name)
        {
            try
            {
                // 422 - Non-string (null binding case)
                if (name == null)
                {
                    return UnprocessableEntity(new
                    {
                        status = "error",
                        message = "Name must be a string"
                    });
                }

                // 400 - Missing or empty
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest(new
                    {
                        status = "error",
                        message = "Name is required"
                    });
                }

                var result = await _service.GetGenderAsync(name);

                // Genderize edge case
                if (result == null || result.Gender == null || result.Count == 0)
                {
                    return Ok(new
                    {
                        status = "error",
                        message = "No prediction available for the provided name"
                    });
                }

                var isConfident = result.Probability >= 0.7 && result.Count >= 100;

                var response = new
                {
                    status = "success",
                    data = new
                    {
                        name = name,
                        gender = result.Gender,
                        probability = result.Probability,
                        sample_size = result.Count,
                        is_confident = isConfident,
                        processed_at = DateTime.UtcNow.ToString("o")
                    }
                };

                return Ok(response);
            }
            catch
            {
                return StatusCode(502, new
                {
                    status = "error",
                    message = "External service error"
                });
            }
        }
    }
}