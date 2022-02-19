# KingsTea
<H3> Kings Tea Application .NET 5 Backend WEB API </H3>

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

        public Task<ServiceResultModel<string>> AddAsync(SalesOrderDto dtoItem)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultModel<string>> DeleteAsync(SalesOrderDto dtoItem)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultModel<SalesOrderEntity>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultModel<SalesOrderEntity>> GetSingleItemAsync(string so)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultModel<string>> UpdateAsync(SalesOrderDto dtoItem)
        {
            throw new NotImplementedException();
        }
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

        public async Task<ServiceResultModel<string>> AddAsync(SalesOrderDto dtoItem) {}

        public async Task<ServiceResultModel<string>> DeleteAsync(SalesOrderDto dtoItem) {}

        public async Task<ServiceResultModel<string>> UpdateAsync(SalesOrderDto dtoItem) {}

        public async Task<ServiceResultModel<SalesOrderEntity>> GetSingleItemAsync(string so) {}

        public async Task<ServiceResultModel<SalesOrderEntity>> GetAllItemsAsync() {}

        private SalesOrderEntity ConvertSalesOrderEntity(SalesOrderDto dtoItem) {}
    }
```
 
 
