using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using miniWms.Api.Services;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Client.Pages;
using miniWms.Components;
using miniWms.Domain.Entities;
using miniWms.Infrastructure;
using miniWms.Infrastructure.Repositories;
using miniWms.Infrastructure.Utilities;
using Sieve.Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISieveProcessor, MiniWmsSieveProcessor>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); ;

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
options.UseSqlServer(builder.Configuration.GetConnectionString("ContainerDb"))); // LocalDb / ContainerDb

builder.Services.AddScoped(typeof(IEmployeesRepository), typeof(EmployeesRepository));
builder.Services.AddScoped(typeof(IWarehousesRepository), typeof(WarehousesRepository));
builder.Services.AddScoped(typeof(IRolesRepository), typeof(RolesRepository));
builder.Services.AddScoped(typeof(ICategoriesRepository), typeof(CategoriesRepository));
builder.Services.AddScoped(typeof(IProductsRepository), typeof(ProductsRepository));
builder.Services.AddScoped(typeof(IContractorsRepository), typeof(ContractorsRepository));
builder.Services.AddScoped(typeof(IDocumentTypesRepository), typeof(DocumentTypesRepository));
builder.Services.AddScoped(typeof(IWarehouseEntriesRepository), typeof(WarehouseEntriesRepository));
builder.Services.AddScoped(typeof(IDocumentEntriesRepository), typeof(DocumentEntriesRepository));
builder.Services.AddScoped(typeof(IDocumentsRepository), typeof(DocumentsRepository));

builder.Services.AddScoped<ITransactionManager, EfTransactionManager>();

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
        new Role() { RoleId = "adm", RoleName = "Admin", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
        new Role() { RoleId = "mng", RoleName = "Manager", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
        new Role() { RoleId = "ope", RoleName = "Operator", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }
    ];
    dbContext.Roles.AddRange(defaultRoles);
}

if (!dbContext.DocumentTypes.Any())
{
    List<DocumentType> defaultDocumentTypes =
    [
        new() { DocumentTypeId = "EXI", ActionType = ActionType.ExternalIssue, DocumentTypeName = "External Issue", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
        new() { DocumentTypeId = "EXR", ActionType = ActionType.ExternalReceipt, DocumentTypeName = "External Receipt", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
        new() { DocumentTypeId = "INT", ActionType = ActionType.InternalTransfer, DocumentTypeName = "Internal Transfer", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
        new() { DocumentTypeId = "INI", ActionType = ActionType.InternalIssue, DocumentTypeName = "Internal Issue", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
        new() { DocumentTypeId = "INR", ActionType = ActionType.InternalReceipt, DocumentTypeName = "Internal Receipt", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }
    ];
    dbContext.DocumentTypes.AddRange(defaultDocumentTypes);
}
dbContext.SaveChanges();

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
