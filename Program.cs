
using TransactionPackage;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Business buss = new Business();

          // route
app.MapGet("/",     () => "Hello World!");

app.MapGet("/timetable", () => "No class today");

app.MapGet("/dinner", () => "No food today");

app.MapGet("/", async c =>
{
    await buss.RestoreTransactions();

    c.Response.WriteAsJsonAsync(buss.TransList);
});


app.MapPost("/addedit", async (Transaction trans) =>
{
    await buss.SaveTransaction(trans);

    return Results.NoContent();
});


app.MapPost("/delete", async (Foo data) =>
{
    await buss.DelTransaction(data.Id);

    return Results.NoContent();
});



app.Run();
