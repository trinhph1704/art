using AWS.Repositories.Interfaces;
using ArtWorkShop.Repositories.Services;
using AWS.Models;
using AWS.Repositories.Interfaces;
using AWS.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    //Add any other JSON serialization settings as needed
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ARTWORKPLATFORMContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("Artwork")));
builder.Services.AddCors(p => p.AddPolicy("MyCors", buid =>
{
    buid.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddScoped<IUser, SUser>();
builder.Services.AddScoped<IArtwork, SArtwork>();
builder.Services.AddScoped<IGenre, SGenre>();
builder.Services.AddScoped<IOrder, SOrder>();
builder.Services.AddScoped<IPayment, SPayment>();
builder.Services.AddScoped<IPremium, SPremium>();
builder.Services.AddScoped<ICollection, SCollection>();
builder.Services.AddScoped<IComment, SComment>();
builder.Services.AddScoped<IReport, SReport>();
builder.Services.AddScoped<IOrderPremium, SOrderPremium>();
builder.Services.AddScoped<IOrderPremiumLog, SOrderPremiumLog>();


builder.Services.AddSwaggerGen(option =>
{

    //set up jwt token authorize
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
});

option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
    }
});
});



// add jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
app.UseSwaggerUI();
}

//if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/AWS/swagger.json", "AWSApi v1"));
//}

app.UseHttpsRedirection();

app.UseCors("MyCors");

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
