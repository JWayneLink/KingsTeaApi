# KingsTea
<H3> Kings Tea Application .NET 5 Backend WEB API</H3>

<ul>
  <li>
    Install-Package Microsoft.EntityFrameworkCore -Version 5.0.1
  </li>
    <li>
    Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.1
  </li>   
</ul>

</hr>

```c#
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "KingsTeaApp", Version = "v1" });
        });

        // Allow CORS
        services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
        });

        // Configuring EF Core
        services.AddDbContextFactory<KTADbContext>(kta => kta.UseSqlServer(Configuration["DefaultConnection"]));            
    }

