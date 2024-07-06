using ListOfItems.DataAccess;
using ListOfItems.Services;
using ListOfItems.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBConnect>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));
builder.Services.AddScoped<IItemRepository, ItemListRepository>();

//Jwt Authencation 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))


    };
});

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Supplieronly", policy => policy.RequireClaim("Role", "Supplier"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireClaim("Role", "Consumer"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // UseAuthentication should be before UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();
