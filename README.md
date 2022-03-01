# KingsTea
<H3> Kings Tea Application .NET 5 Backend WEB API </H3>

- IDE: Visual Studio 2022

<H4>MSSQL Database</H4>
<ul>
  <li>
    Install-Package Microsoft.EntityFrameworkCore -Version 5.0.1
  </li>
    <li>
    Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.1
  </li>   
</ul>

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
```

<H4>Database table schema design</H4>

![KingsTeaAppDatabaseEntity](https://user-images.githubusercontent.com/40432032/155993230-4498c5df-4db5-4258-ab88-bb8ad29dd6ec.png)


<hr>
    
<H4>IoC Framework</H4>
<ul>
  <li>
    Install-Package Autofac -Version 6.3.0
  </li>
    <li>
    Install-Package Autofac.Extensions.DependencyInjection -Version 7.2.0
  </li>   
</ul>

```c#
    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
      .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // .NET 5 Autofac as the DI container
      .ConfigureWebHostDefaults(webBuilder =>
      {
          webBuilder.UseStartup<Startup>();
      });
            
    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new AutofacModule());
    }
    
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Servie DI Register
            builder.RegisterType<DateTimeService>().As<IDateTimeService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<SalesOrderService>().As<ISalesOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();

            // Repository DI Register
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SalesOrderRepository>().As<ISalesOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
        }
    }
```
```c#
    // 1. 建立Autofac容器
    ContainerBuilder builder = new ContainerBuilder();

    // 2.註冊型別(可限制創建物件生命週期)
    builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
    builder.Populate(services);

    // 3.建立IContainer
    IContainer container = builder.Build();
```

<hr>

<H4>Http Policy Extensions Register</H4>
<ul>
  <li>
    Install-Package Microsoft.Extensions.Http -Version 5.0.0
  </li>
    <li>
    Install-Package Microsoft.Extensions.Http.Polly -Version 2.2.0
  </li>   
</ul>

```c#
    // Add http client CustomerService
    services.AddHttpClient<ICustomerService, CustomerService>(client =>
    {
        client.BaseAddress = new Uri(Configuration["PlaceholderUsers"]);                
    })
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCircuitBreakerPolicy())
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

    // Add http client ProductService
    services.AddHttpClient<IProductService, ProductService>(client =>
    {
        client.BaseAddress = new Uri(Configuration["PlaceholderAlbums"]);
    })
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCircuitBreakerPolicy())
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));
            
    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
```
<hr>

<H4>Http Policy Extensions Register</H4>
<ul>
  <li>
    Install-Package Microsoft.AspNet.WebApi.Core -Version 5.2.7
  </li>
</ul>

```c#
    // Register filter
    services.AddScoped<ValidationFilterAttribute>();

    // 設定藉由filter判定ModelState.IsValid的檢查
    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {           
            if (!context.ModelState.IsValid)
            {             
                if (
                    context.ModelState.Values.FirstOrDefault() != null && 
                    context.ModelState.Values.First().Errors.FirstOrDefault() != null
                )
                {
                    // context.ModelState.Values.First().Errors.First().ErrorMessage
                    string valideErrorMsg = context.ModelState.Values.First().Errors.First().ErrorMessage;

                    // 既使ModelState.InValid,可回傳自訂的StatusCode & response JSON
                    JObject jObj = new JObject();
                    jObj["isSuccess"] = false;
                    jObj["message"] = valideErrorMsg;
                    jObj["data"] = string.Empty;
                    string json = JsonConvert.SerializeObject(jObj);

                    context.Result = new ContentResult()
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.OK,
                        Content = json,
                        ContentType = "application/json"
                    };
                }
            }
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
```

<hr>

<H4>Allow CORS settings</H4>

```c#
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseAuthorization();

        // allow CORS
        app.UseCors(options => options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        //.SetIsOriginAllowed(origin => true) // allow any origin
        //.AllowCredentials() // allow credentials
         );

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
```

<hr>

<H4>Inheritance Design Pattern - Repository</H4>

```c#
    public interface IRepositoryBase<TEntity>
    {
        Task<int> AddAsync(TEntity item);
        Task<int> UpdateAsync(TEntity item);
        Task<TEntity> GetSingleItemAsync(TEntity item);
        Task<IEnumerable<TEntity>> GetAllItemsAsync();
        Task<int> DeleteAsync(TEntity item);
    }
    
    public interface ISalesOrderRepository : IRepositoryBase<SalesOrderEntity>
    {
        Task<SalesOrderEntity> GetSingleItemAsync(string so);
    }
    
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly IDbContextFactory<KTADbContext> _ctx;

        public SalesOrderRepository(IDbContextFactory<KTADbContext> ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> AddAsync(SalesOrderEntity item) {...}

        public async Task<int> DeleteAsync(SalesOrderEntity item) {...}

        public async Task<IEnumerable<SalesOrderEntity>> GetAllItemsAsync() {...}

        public async Task<SalesOrderEntity> GetSingleItemAsync(SalesOrderEntity item) {...}

        public async Task<SalesOrderEntity> GetSingleItemAsync(string so) {...}

        public async Task<int> UpdateAsync(SalesOrderEntity item) {...}
    }
```

<hr>

<H4>Inheritance Design Pattern - Service</H4>
 
 ```C#
    public interface ISalesOrderService : IService
    {
        Task<ServiceResultModel<string>> AddAsync(SalesOrderDto dtoItem);
        Task<ServiceResultModel<string>> DeleteAsync(SalesOrderDto dtoItem);
        Task<ServiceResultModel<string>> UpdateAsync(SalesOrderDto dtoItem);
        Task<ServiceResultModel<SalesOrderEntity>> GetSingleItemAsync(string so);
        Task<ServiceResultModel<SalesOrderEntity>> GetAllItemsAsync();
    }    
    
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public SalesOrderService(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task<ServiceResultModel<string>> AddAsync(SalesOrderDto dtoItem) {...}

        public async Task<ServiceResultModel<string>> DeleteAsync(SalesOrderDto dtoItem) {...}

        public async Task<ServiceResultModel<string>> UpdateAsync(SalesOrderDto dtoItem) {...}

        public async Task<ServiceResultModel<SalesOrderEntity>> GetSingleItemAsync(string so) {...}

        public async Task<ServiceResultModel<SalesOrderEntity>> GetAllItemsAsync() {...}

        private SalesOrderEntity ConvertSalesOrderEntity(SalesOrderDto dtoItem) {...}
    }
```
 </hr>
 
  <H4>SalesOrderController</H4>
 
 ```C#
    [Route("v1/api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;
        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        [HttpPost, Route("AddSalesOrderAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> AddSalesOrderAsync(SalesOrderDto addSalesOrderDto) {...}

        [HttpPut, Route("UpdateSalesOrderAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> UpdateSalesOrderAsync(SalesOrderDto updateSalesOrderDto) {...}

        [HttpDelete, Route("DeleteSalesOrderAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> DeleteSalesOrderAsync(SalesOrderDto deleteSalesOrderDto) {...}

        [HttpGet, Route("GetSingleSalesOrderAsync")]
        public async Task<ApiResultModel<SalesOrderEntity>> GetSingleSalesOrderAsync(string so) {...}

        [HttpGet, Route("GetAllSalesOrdersAsync")]
        public async Task<ApiResultModel<SalesOrderEntity>> GetAllSalesOrdersAsync() {...}

 ```

</hr>
 
![image](https://user-images.githubusercontent.com/40432032/155993601-0fc29aec-11bf-49b3-9232-a0925ce61265.png)


<H4>AppAccount Http Methods</H4>

![image](https://user-images.githubusercontent.com/40432032/155993632-99c7e3f8-a932-488e-bb35-8cffb0d66e3f.png)


<H4>Customer Http Methods</H4>

![image](https://user-images.githubusercontent.com/40432032/155993690-d5b2b1cc-d5f8-4b56-bdfa-7eea50d1bae2.png)


<H4>Product Http Methods</H4>

![image](https://user-images.githubusercontent.com/40432032/155993756-d8c86d5c-2fa4-4774-b180-95cc6b070ddf.png)


<H4>SalesOrder Http Methods</H4>

![image](https://user-images.githubusercontent.com/40432032/155993857-e03968f7-1a2f-4015-9d12-3b86d6589da2.png)

