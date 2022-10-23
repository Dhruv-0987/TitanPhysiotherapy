using Microsoft.EntityFrameworkCore;
using TitanPhysiotherapy.Database;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Services.PatientService;
using TitanPhysiotherapy.Services.UserService;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using TitanPhysiotherapy.Services.StaffService;
using TitanPhysiotherapy.Services.TreatmentService;
using TitanPhysiotherapy.Services.RatingService;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IStaffInterface, StaffService>();
builder.Services.AddScoped<ITreatmentInterface, TreatmentService>();
builder.Services.AddScoped<IRatingInterface, RatingService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie()
  .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
  {
      options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
      options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
      options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
  });
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
