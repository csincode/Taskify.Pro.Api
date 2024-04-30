using Taskfy.Pro.Api.Models;
using Taskfy.Pro.Api.Services;
using Taskfy.Pro.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StoreDatabaseSettings>(
    builder.Configuration.GetSection("StoreDatabaseSettings"));

builder.Services.AddSingleton<UsersService>();

var app = builder.Build();

app.MapGet("/api/users", async (UsersService usersService) =>
{
    var users = await usersService.GetAsync();
    return users;
});

app.MapGet("/api/users/{id:length(24)}", async (string id, UsersService usersService) =>
{
    var user = await usersService.GetAsync(id);
    if (user is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(user);
});

app.MapPost("/api/users", async (User newUser, UsersService usersService) =>
{
    await usersService.CreateAsync(newUser);
    return Results.CreatedAtRoute("/api/users/{id}", new { id = newUser.Id.ToString() }, newUser.Id);
});

app.MapPut("/api/users/{id:length(24)}", async (string id, User updatedUser, UsersService usersService) =>
{
    var existingUser = await usersService.GetAsync(id);
    if (existingUser is null)
    {
        return Results.NotFound();
    }
    updatedUser.Id = existingUser.Id;
    await usersService.UpdateAsync(id, updatedUser);
    return Results.NoContent();
});

app.MapDelete("/api/users/{id:length(24)}", async (string id, UsersService usersService) =>
{
    var existingUser = await usersService.GetAsync(id);
    if (existingUser is null)
    {
        return Results.NotFound();
    }
    await usersService.RemoveAsync(id);
    return Results.NoContent();
});

app.Run();
