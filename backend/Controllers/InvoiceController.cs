using Microsoft.AspNetCore.Mvc;
using test_finix.Models;

namespace test_finix.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly ILogger<InvoiceController> _logger;

    public InvoiceController(ILogger<InvoiceController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Invoice> Get()
    {
        return null;
    }
}
