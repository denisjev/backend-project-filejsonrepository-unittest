using MediatR;
using System.Reflection;
using Contexts.Users.Application.DTOs;
using Contexts.Users.Domain;
using Contexts.Users.Infraestruture;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowAllOrigins = "_myAllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAllOrigins,
        policy  =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();;
        });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserDTO).Assembly));
builder.Services.AddScoped<IUserRepository, UserRepostoryFromFile>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowAllOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();