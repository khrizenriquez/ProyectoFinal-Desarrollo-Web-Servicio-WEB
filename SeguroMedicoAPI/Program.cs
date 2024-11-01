using Microsoft.EntityFrameworkCore;
using SeguroMedicoAPI.Data;
using SeguroMedicoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SeguroMedicoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SeguroMedicoDB")));
builder.Services.AddScoped<IConsultaCoberturaService, ConsultaCoberturaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Elimina la siguiente l�nea para deshabilitar HTTPS redirection
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
