
using ExamCertify.Application;
using ExamCertify.Application.Interfaces.Courses;
using ExamCertify.Application.Services;
using ExamCertify.Infrastructure;
using ExamCertify.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace ExamCertify.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Configuring Services-Start
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ExamCertifyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure());
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<ICourseRepository, CoursesRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            // In production, modify this with the actual domains you want to allow
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            #endregion Configuring Services-End

            #region Configuring Pipeline-Start
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            app.UseCors("default");
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("My Api");
                    options.WithTheme(ScalarTheme.BluePlanet);
                    options.WithSidebar(true);
                });
                app.UseSwaggerUi(options =>
                {
                    options.DocumentPath = "openapi/v1.json";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion Configuring Pipeline-End
        }
    }
}
