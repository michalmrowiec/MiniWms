using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using miniWms.Application.Contracts;
using miniWms.Client.Pages;
using miniWms.Components;
using miniWms.Domain.Entities;
using miniWms.Infrastructure;
using miniWms.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddControllersWithViews();

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
        ValidateLifetime = true
    };
});

builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddDbContext<MiniWmsDbContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MiniWmsDb;Trusted_Connection=True;"));

builder.Services.AddScoped(typeof(IEmployeesRepository), typeof(EmployeesRepository));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<MiniWmsDbContext>();
var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

if (!dbContext.Roles.Any())
{
    List<Role> defaultRoles =
    [
        new Role() { RoleId = "adm", RoleName = "Admin", CreatedAt= DateTime.Now, ModifiedAt = DateTime.Now },
        new Role() { RoleId = "mng", RoleName = "Manager", CreatedAt= DateTime.Now, ModifiedAt = DateTime.Now },
        new Role() { RoleId = "ope", RoleName = "Operator", CreatedAt= DateTime.Now, ModifiedAt = DateTime.Now }
    ];
    dbContext.Roles.AddRange(defaultRoles);
    dbContext.SaveChanges();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiniWmsAPI"));
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
