using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Domain;
using MinimalApi.Dto;
using MinimalApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/books", async ([FromServices] IBookRepository bookRepo, [FromServices] IMapper mapper) =>
{
    var books = await bookRepo.GetAll();
    var result = mapper.Map<List<GetBookDto>>(books);
    return Results.Ok(result);
}).WithName("GetAllBook");

app.MapGet("/api/books/{id}", async ([FromRoute] int id,
    [FromServices] IBookRepository bookRepo, [FromServices] IMapper mapper) =>
{
    var book = await bookRepo.GetById(id);
    if (book==null)
    {
        return Results.NotFound();
    }
    var result = mapper.Map<GetBookDto>(book);
    return Results.Ok(result);
});

app.MapPost("/api/books", async ([FromBody] BookDto request,
    [FromServices] IBookRepository bookRepo, [FromServices] IMapper mapper) =>
{
    var book = mapper.Map<Book>(request);
    await bookRepo.Insert(book);
    await bookRepo.SaveChangeAsync();
    return Results.Created("/api/books", null);
});

app.MapPut("/api/books/{id}", async ([FromRoute] int id, [FromBody] BookDto request,
    [FromServices] IBookRepository bookRepo, [FromServices] IMapper mapper) =>

{
    var book = await bookRepo.GetById(id);
    if (book == null)
    {
        return Results.NotFound();
    }

    book = mapper.Map(request, book);
    await bookRepo.SaveChangeAsync();
    return Results.Created($"/api/books/{book.Id}", book);
});


app.MapDelete("/api/books/{id}", async ([FromRoute] int id, 
    [FromServices] IBookRepository bookRepo) =>
{
    var book = await bookRepo.GetById(id);
    if (book == null)
    {
        return Results.NotFound();
    }

    bookRepo.Delete(book);
    await bookRepo.SaveChangeAsync();
    return Results.NoContent();
});

app.Run();