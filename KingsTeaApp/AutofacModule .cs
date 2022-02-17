﻿using Autofac;
using KTA.Data.Repository;
using KTA.Data.Service;
using KTA.Model.Interface;
using KTA.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingsTeaApp
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method. All of this starts
            // with the services.AddAutofac() that happens in Program and registers Autofac
            // as the service provider.

            // SingleInstance() 被實例化後就不會消失，程式運行期間只會有一個實例。
            // InstancePerLifetimeScope() 獨立容器，在使用完畢後可任意Dispose()而不會影響IContainer。

            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeService>().As<IDateTimeService>().InstancePerLifetimeScope();

            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            

        }
    }
}
