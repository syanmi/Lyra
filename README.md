# 🌟 Lyra - Lightweight, AOT-Ready Web API Framework for .NET

**Lyra** is a **minimal**, **fast**, and **AOT-friendly** web API framework for C#.
Inspired by Express.js, it’s designed for **microservices**, **internal tools**, and **self-hosted apps** that need **performance without the bloat**.

> ✅ Express-like routing and middleware — in pure C#  
> ✅ Self-hosted with `HttpListener` — no Kestrel or ASP.NET  
> ✅ AOT-friendly JSON binding — works with source generators  
> ✅ Lightweight and composable — just one DLL and you're ready

## 🚀 Why Lyra?

| Feature                         | Lyra        | ASP.NET Minimal API | Express.js   |
|----------------------------------|-------------|----------------------|--------------|
| Lightweight & self-hosted     | ✔️           | ❌ (Kestrel)          | ✔️            |
| AOT-friendly (no reflection)  | ✔️           | ⚠️ Partial             | ❌            |
| Express-like simplicity       | ✔️           | ❌                    | ✔️            |
| Minimal dependencies          | ✔️           | ❌                    | ✔️            |
| Good for internal tools/CLI   | ✔️           | ❌                    | ✔️            |

## 🚀 Get Started Instantly

You don’t need a package.
Just clone the repo and run a sample:

```bash
git clone https://github.com/syanmi/Lyra.git
cd Lyra/SampleConsoleApp/
dotnet run
```

Or create a simple app like this:

```csharp
var app = new LyraApp();

app.Get("/hello", ctx => ctx.Text("Hello from Lyra!"));

await app.RunAsync();
```

- No NuGet installation needed  
- No build templates or scaffolding tools  
- Just C# and .NET — ready to go

---

## 🧬 AOT-Ready JSON Binding

```csharp
app.UseJsonContext(MyJsonContext.Default);

[JsonSerializable(typeof(MyRequest))]
public partial class MyJsonContext : JsonSerializerContext {}
```

- Uses `System.Text.Json` source generators  
- Reflection-free & trimming-friendly  
- Ideal for native AOT builds

---

## 🧱 Middleware and SubApps

```csharp
var sub = new LyraApp();
sub.Get("/hi", ctx => ctx.Text("Hello from SubApp!"));

app.Use(async (ctx, next) =>
{
    Console.WriteLine("Before SubApp");
    await next();
});

app.UseSubApp("/sub", sub);
```

- Compose multiple apps like Express's `app.use('/path', sub)`
- Build modular and reusable APIs


## 📂 Static File Serving

```csharp
app.UseStaticFiles("wwwroot");
```

- Serve static files for admin tools or dashboards

## 📦 Current Features

| Feature              | Status |
|----------------------|--------|
| GET/POST Routing     | ✅     |
| Path Parameters      | ✅     |
| JSON Binding (AOT)   | ✅     |
| Middleware           | ✅     |
| SubApps              | ✅     |
| Static Files         | ✅     |
| CLI Integration      | 🚧 *(coming soon)*  
| NuGet Package        | 🚧 *(planned for v1.0)*

## 📌 Roadmap

| Version | Features                                             |
|---------|------------------------------------------------------|
| v0.1    | Basic routing, responses                             |
| v0.2    | Path params, body reader                             |
| v0.3    | `ReadBodyAsync<T>()` with AOT-ready JSON             |
| v0.4    | Middleware, subapps                                  |
| v0.5    | static files                                         |
| v1.0    | Logging, CORS, file download, CLI scaffolding        |

> CLI tools (`dotnet new lyra`) and NuGet packaging are planned for **v1.0**

---

## 📄 License

MIT License.  
Contributions welcome!

## ⭐ Like Lyra?

If Lyra helps you build faster and smaller .NET apps, a ⭐ would mean a lot!

## ✅ Summary

Lyra is for developers who want:

- Fast startup and small output size  
- Native AOT-ready web APIs  
- Express-like simplicity in C#  
- No heavy dependencies or runtime costs

## 🔗 Related

- [Express.js](https://expressjs.com/)
- [ASP.NET Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
- [System.Text.Json Source Generators](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)

