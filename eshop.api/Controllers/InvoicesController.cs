using eshop.application.DTOs.RequestDTOs;
using eshop.application.Services;
using Microsoft.AspNetCore.Mvc;

namespace eshop.api.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceService _service;
        public InvoicesController(InvoiceService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceRequest request)
        {
            var dto = await _service.CreateInvoiceAsync(request);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id) => NotFound(); // implement repo query if needed
    }
}