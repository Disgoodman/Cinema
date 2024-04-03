using Cinema;
using Cinema.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var services = builder.Services;

var exportSqlOptions = builder.Configuration.GetSection(nameof(ExportSqlOptions));
services.Configure<ExportSqlOptions>(exportSqlOptions);
var connectionString = exportSqlOptions.GetValue<string>(nameof(ExportSqlOptions.ConnectionString));
services.AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
services.AddDatabaseDeveloperPageExceptionFilter();

services.AddIdentity<IdentityUser, IdentityRole>(identityOptions => identityOptions.SignIn.RequireConfirmedAccount = false)
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

services.AddRazorPages();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var serviceScope = app.Services.CreateScope())
    {
        using var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }

    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();