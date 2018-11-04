using AspNetCore.Security.Jwt.Microservice.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCore.Security.Jwt.Microservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add dependency injection
            services.AddDependencyInjection(this.Configuration);

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "XXX API", Version = "v1" });
            });

            //services.AddSecurity<Authenticator>(this.Configuration, true);
            //services.AddMvc().AddSecurity();

            //services.AddSecurity<Authenticator, UserModel>(this.Configuration, builder =>
            //    builder.AddClaim(IdType.Name, userModel => userModel.Id)
            //           .AddClaim(IdType.Role, userModel => userModel.Role)
            //           .AddClaim("DOB", userModel => userModel.DOB.ToShortDateString())
            //, true);

            services
            .AddSecurity<Authenticator, UserModel>(this.Configuration, builder =>
                builder.AddClaim(IdType.Name, userModel => userModel.Id)
                       .AddClaim(IdType.Role, userModel => userModel.Role)
                       .AddClaim("DOB", userModel => userModel.DOB.ToShortDateString())
            , true)
            .AddFacebookSecurity(this.Configuration, builder =>
                builder.AddClaim("FacebookUser", userModel => userModel.UserAccessToken)
            , true);

            services.AddMvc().AddSecurity<UserModel>().AddFacebookSecurity();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                            builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                        );

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "XXX API V1");
            });

            app.UseSecurity(true);

            app.UseMvc();
        }
    }
}
