
using TransactionPackage;
// need to install Firebaseadmin and Google.Cloud.Firestore sdk

var builder = WebApplication.CreateBuilder(args);

// NEED THIS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials();
        });
});


var app = builder.Build();

Business buss = new Business();
buss.initFirestore();

          // route
app.MapGet("/",     () => "Hello World!");

app.MapGet("/timetable", () => "No class today");

app.MapGet("/dinner", () => "No food today");

app.MapGet("/transactions", async c =>
{
    await buss.RestoreTransactions();

    c.Response.WriteAsJsonAsync(buss.TransList.transactions);
});


app.MapPost("/transactions/addedit", async (Transaction trans) =>
{
    await buss.SaveTransaction(trans);

    return Results.NoContent();
});


app.MapPost("/transactions/delete", async (Foo data) =>
{
    await buss.DelTransaction(data.Id);

    return Results.NoContent();
});

// NEED THIS
app.UseCors("AllowAll");

app.Run();
