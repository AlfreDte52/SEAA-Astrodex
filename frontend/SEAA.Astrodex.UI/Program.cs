using SEAA.Astrodex.UI.Components;
using SEAA.Astrodex.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// nustro enlace con la API de astrodex para no tener que escribir la url cada vez que queramos hacer una petición
builder.Services.AddHttpClient(
    "AstrodexAPI",
    client =>
    {
        client.BaseAddress =
            new Uri(
                "https://localhost:7065/"
            );
    });

builder.Services.AddScoped<
    CuerpoCelesteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
