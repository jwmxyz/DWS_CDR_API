using Cdr.Api.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterApplicationServices(builder);

var app = builder.Build();

app.ConfigureMiddleware();
app.RegisterEndpoints();

app.Run();
