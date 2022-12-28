using Lamorenita;
using Lamorenita.Data_Contexts;
using Lamorenita.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Se agregan los servicios de la app
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddAppServices();

// db connection from appsettings
builder.Services.AddDbContext<LamorenitaDbContext>(
    opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DataDbContext_Connection"))
        .EnableSensitiveDataLogging(true).UseLazyLoadingProxies();
    }
    );

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

using var scope = app.Services.CreateScope();
/*
var db = scope.ServiceProvider.GetRequiredService<LamorenitaDbContext>();
db.Database.Migrate();
*/
app.Run();
