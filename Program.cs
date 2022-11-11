using AutoMapper;
using Lamorenita;
using Lamorenita.Data_Contexts;
using Lamorenita.Services;
using Lamorenita.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// db connection from appsettings
builder.Services.AddDbContext<LamorenitaDbContext>(
    opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DataDbContext_Connection"))
        .EnableSensitiveDataLogging(true).UseLazyLoadingProxies();
    }
    );
//Automapper profile
builder.Services.AddSingleton(provider =>
{
    return new MapperConfiguration(config =>
    {
        config.AddProfile<AutoMapperProfile>();
        config.ConstructServicesUsing(type =>
        ActivatorUtilities.GetServiceOrCreateInstance(provider, type));
    }).CreateMapper();
});

// add to call api
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IDirectionService, DirectionService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IPhoneNumberService, PhoneNumberService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
