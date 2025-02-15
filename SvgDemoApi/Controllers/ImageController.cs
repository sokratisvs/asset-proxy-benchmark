using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public ImageController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet("cdn/{file}")]
    public IActionResult GetStaticFile(string file)
    {
        string path = Path.Combine(_env.WebRootPath, "static", file);
        if (!System.IO.File.Exists(path))
            return NotFound();

        return PhysicalFile(path, "image/svg+xml");
    }

    [HttpGet("proxy/{file}")]
    public async Task<IActionResult> GetModifiedSvg(string file, [FromQuery] string color = "black")
    {
        string path = Path.Combine(_env.WebRootPath, "icons", file);
        if (!System.IO.File.Exists(path))
            return NotFound();

        string svg = await System.IO.File.ReadAllTextAsync(path);
        svg = svg.Replace("#000000", color);

        return Content(svg, "image/svg+xml");
    }
}