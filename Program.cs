var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Enable CORS for development
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularDev",
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:4200") // Angular development URL
//                   .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
//                   .AllowAnyHeader()
//                   .AllowCredentials(); // Allow any headers
//        });
//});




// 1. Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFirebaseOrigin",
        policy =>
        {
            policy.WithOrigins("https://sampleproject-8950c.web.app") // your Firebase URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowAngularDev");
app.UseStaticFiles(); // needed if you're storing in wwwroot


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFirebaseOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
