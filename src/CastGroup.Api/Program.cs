using CastGroup.Api.Data;
using CastGroup.Api.Models;
using CastGroup.Api.Services;
using Microsoft.EntityFrameworkCore;

#region Configure
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opt.RoutePrefix = string.Empty;
});

MapActions();
app.Run();

#endregion

#region MapActions
void MapActions()
{
    app.MapGet("/viacep/{cep}", async (string cep) =>
        await ViaCepService.GetViaCep(cep))
        .Produces<ViaCepResponse>();

    app.MapGet("/accounts", async (DataContext context) =>
        await context.Accounts.ToListAsync())
        .Produces<IEnumerable<Account>>();

    app.MapGet("/account/{id}", async (Guid id, DataContext context) =>
        await context.Accounts.FindAsync(id)
            is Account account
                ? Results.Ok(account)
                : Results.NotFound())
        .Produces<Account>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

    app.MapPost("/account", async (Account account, DataContext context) =>
    {
        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        return Results.Created($"/account/{account.Id}", account);
    }).Produces<Account>(StatusCodes.Status201Created);

    app.MapPut("/account/{id}", async (Guid id, Account inputTodo, DataContext context) =>
    {
        var todo = await context.Accounts.FindAsync(id);

        if (todo is null) return Results.NotFound();

        todo.Name = inputTodo.Name;
        todo.Description = inputTodo.Description;

        await context.SaveChangesAsync();

        return Results.NoContent();
    }).Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);

    app.MapDelete("/account/{id}", async (Guid id, DataContext context) =>
    {
        if (await context.Accounts.FindAsync(id) is Account todo)
        {
            context.Accounts.Remove(todo);
            await context.SaveChangesAsync();
            return Results.NoContent();
        }

        return Results.NotFound();
    }).Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);
}
#endregion
