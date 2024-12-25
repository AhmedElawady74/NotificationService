using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// إعداد Kestrel مع المنفذ
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5181);
});

// إضافة الخدمات
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourIssuer", // ضع القيم الصحيحة هنا
            ValidAudience = "yourAudience", // ضع القيم الصحيحة هنا
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSecretKey")) // ضع القيم الصحيحة هنا
        };
    });

var app = builder.Build();

// إعداد Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// تفعيل المصادقة
app.UseAuthentication();
app.UseAuthorization();

// إضافة Middleware المخصص لمعالجة الأخطاء
app.UseMiddleware<NotificationGateway.Middlewares.ErrorHandlingMiddleware>();

// تفعيل Serilog Middleware (اختياري)
app.UseSerilogRequestLogging();

app.MapGet("/", async context =>
{
    var html = @"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Notification Gateway</title>
        <link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css'>
    </head>
    <body class='bg-light text-center'>
        <div class='container py-5'>
            <h1 class='text-primary'>Welcome to Notification Gateway!</h1>
            <p class='lead'>This service helps you send notifications via Email, SMS, and Push Notifications.</p>
            <a href='/swagger' class='btn btn-primary btn-lg'>Explore API Documentation</a>
        </div>
        <footer class='text-muted mt-5'>
            <small>© 2024 Notification Gateway Team</small>
        </footer>
    </body>
    </html>";
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(html);
});
app.MapControllers();

app.Run();
