using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models.ApplicationDbContext;
using SAMLitigation.Models.AuthorizeAttribute;
using SAMLitigation.Services;
using SAMLitigation.Services.ServiceImple;

var builder = WebApplication.CreateBuilder(args);

// Register AppDbContext with connection string
var connectionString = builder.Configuration.GetConnectionString("IDCOLMISConnection");
builder.Services.AddDbContext<SAMDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Services
builder.Services.AddScoped<UserTableService, UserTableServiceImple>();
builder.Services.AddScoped<AuthenticateService, AuthenticateServiceImple>();
builder.Services.AddScoped<CauseService, CauseServiceImple>();
builder.Services.AddScoped<CourtService, CourtServiceImple>();
builder.Services.AddScoped<DashboardService, DashboardServiceImple>();
builder.Services.AddScoped<LawyerService, LawyerServiceImple>();
builder.Services.AddScoped<StatusService, StatusServiceImple>();
builder.Services.AddScoped<TypeService, TypeServiceImple>();
builder.Services.AddScoped<DynamicMenuService, DynamicMenuServiceImple>();
builder.Services.AddScoped<SectorService, SectorServiceImple>();
builder.Services.AddScoped<LitigationActionService, LitigationActionServiceImple>();
builder.Services.AddScoped<LitigationDetailDocumentListService, LitigationDetailDocumentListServiceImple>();
builder.Services.AddScoped<LitigationMasterService, LitigationMasterServiceImple>();
builder.Services.AddScoped<LitigationDetailService, LitigationDetailServiceImple>();
builder.Services.AddScoped<LoanProjectService, LoanProjectServiceImple>();
builder.Services.AddScoped<LitigationPartyService, LitigationPartyServiceImple>();
builder.Services.AddScoped<ProjectTypeService, ProjectTypeServiceImple>();

// Register NEW Dynamic Menu Service
builder.Services.AddScoped<DynamicMenuService, DynamicMenuServiceImple>();

builder.Services.AddHttpClient<AuthenticateServiceImple>();
builder.Services.AddScoped<CustomAuthorizationFillter>();

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();