using Lyra;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Lyra.Middleware;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var app = new LyraApp();

app.UseJsonContext(new WeatherForecastContext());
app.UseExceptionHandler();

app.Use(async (ctx, next) =>
{
    var sw = Stopwatch.StartNew();
    Console.WriteLine($"[ROOT]");
    await next();
    Console.WriteLine($"[ROOT] [{ctx.Request.Method}] {ctx.Request.Path} - {sw.ElapsedMilliseconds}ms");
});

app.Get("/hello", 
    ctx => Task.FromResult(LyraResult.Text("Hello from Lyra!")));

app.Post("/echo", async ctx =>
{
    var body = await ctx.Request.ReadBodyAsync();
    return LyraResult.Text("You said: " + body);
});


app.Get("/company/{cid}/employee/{eid}", ctx => {
    var cid = ctx.Request.RouteValues["cid"];
    var eid = ctx.Request.RouteValues["eid"];
    return Task.FromResult(LyraResult.Json<TestData>(new TestData() { Cid = cid, Eid = eid }));
});

app.Post<TestData>("/company/{cid}/employee/{eid}", (LyraContext ctx, TestData dto) =>
{
    var result = LyraResult.Text($"Request Body : Cid[{dto.Cid}] Eid[{dto.Eid}]");
    return Task.FromResult(result);
});



app.Map("/api", api =>
{
    api.Get("/hello",
        ctx => Task.FromResult(LyraResult.Text("Hello from Lyra!")));

    api.Post("/echo", async ctx =>
    {
        var body = await ctx.Request.ReadBodyAsync();
        return LyraResult.Text("You said: " + body);
    });
});

app.Map("/userapi/{id}", user =>
{
    user.Use(async (ctx, next) =>
    {
        var sw = Stopwatch.StartNew();
        Console.WriteLine($"[USERAPI]");
        await next();
        Console.WriteLine($"[USERAPI] [{ctx.Request.Method}] {ctx.Request.Path} - {sw.ElapsedMilliseconds}ms");
    });

    user.Get("/hello",
        ctx =>
        {
            var id = ctx.Request.RouteValues["id"];
            return Task.FromResult(LyraResult.Text($"Hello from Lyra! id:{id}"));
        });

    user.Post("/echo", async ctx =>
    {
        var body = await ctx.Request.ReadBodyAsync();
        return LyraResult.Text("You said: " + body);
    });
});

await app.RunAsync(8080);

while (true) { }

[JsonSerializable(typeof(TestData))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(int))]
public partial class WeatherForecastContext : JsonSerializerContext
{
}

public class TestData
{
    [JsonRequired]
    public string Cid { get; set; } = string.Empty;
    public string Eid { get; set; } = string.Empty;
}
