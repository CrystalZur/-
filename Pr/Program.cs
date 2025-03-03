List<Game> repo = new List<Game>
{
    new() {Name = "aboab", ReleaseDate = DateTime.Now, Genre = "Ужас", Price = 99.99}
};

var builder = WebApplication.CreateBuilder();

builder.Services.AddCors(); 

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapGet("/gr", () =>  repo);

app.MapPost("/gc", (Game game) => repo.Add(game));

app.MapPut("/gu", (GameUpdateDTO dto) =>
{
    var game = repo.FirstOrDefault(o => o.Name == dto.Name);
    if (game != null)
    {
        game.ReleaseDate = dto.ReleaDate;
        game.Price = dto.Price;
        return Results.Json(game);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("/gd/{name}", (string name) =>
{
    Game? game = repo.FirstOrDefault(o => o.Name == name);
    if (game != null)
    {
        repo.Remove(game);
        return Results.Json(game);
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

record class GameUpdateDTO(string Name, DateTime ReleaDate, double Price);

class Game
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; }
    public double Price { get; set; }
}
