using PetShop.EF.Context;
using PetShop.EF.Repos;
using PetShop.Model;
using PetShop.Web.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PetShopContext>();
builder.Services.AddScoped<CustomerRepo>();
builder.Services.AddScoped<IEntityRepo<Employee>, EmployeeRepo>();
builder.Services.AddScoped<IEntityRepo<Pet>, PetRepo>();
builder.Services.AddScoped<IEntityRepo<PetFood>, PetFoodRepo>();
builder.Services.AddScoped<IEntityRepo<Transaction>, TransactionRepo>();
builder.Services.AddScoped<IEntityRepo<PetReport>, PetReportRepo>();
builder.Services.AddScoped<PetReportHandler>();
builder.Services.AddScoped<IEntityRepo<MonthlyLedger>, MonthlyLedgerRepo>();
builder.Services.AddScoped<MonthlyLedgerHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
