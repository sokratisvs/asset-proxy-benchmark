using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Serve wwwroot
var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "../wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(wwwrootPath),
    RequestPath = "/wwwroot"
});

var cache = new MemoryCache(new MemoryCacheOptions());

app.MapGet("/", async context =>
{
    var filePath = Path.Combine("client", "index.html");
    if (File.Exists(filePath))
        await context.Response.SendFileAsync(filePath);
    else
        context.Response.StatusCode = StatusCodes.Status404NotFound;
});

// Serve static SVGs from /static/
app.MapGet("/cdn/{file}", async (HttpContext context, string file) =>
{
    string path = Path.Combine("wwwroot/static", file);
    if (!File.Exists(path)) return Results.NotFound();
    return Results.File(path, "image/svg+xml");
});

// Modify SVGs dynamically from /icons/
app.MapGet("/proxy/{file}", async (HttpContext context, string file, string color = "black") =>
{
    string cacheKey = $"{file}-{color}";
    if (cache.TryGetValue(cacheKey, out string cachedSvg)) return Results.Text(cachedSvg, "image/svg+xml");

    string path = Path.Combine("wwwroot/icons", file);
    if (!File.Exists(path)) return Results.NotFound();

    string svg = await File.ReadAllTextAsync(path);
    svg = svg.Replace("#000000", color);

    cache.Set(cacheKey, svg, TimeSpan.FromMinutes(10));
    return Results.Text(svg, "image/svg+xml");
});

// Serve brand-specific images
app.MapGet("/images/{brand}/{file}", async (HttpContext context, string brand, string file) =>
{
    string path = Path.Combine("wwwroot/images", brand, file);
    if (!File.Exists(path)) return Results.NotFound();

    var contentType = "image/jpeg";
    if (file.EndsWith(".png")) contentType = "image/png";
    
    return Results.File(path, contentType);
});

// app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
