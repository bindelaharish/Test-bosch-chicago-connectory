using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RB.JobAssistant.Core
{
    public static class AutofacStartupExtension
    {
        public static IContainer AddAutofacContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var assembly = Assembly.GetExecutingAssembly();
            //load all dependencies via Module. A Module class is added in all core folders
            builder.RegisterAssemblyModules(assembly);

            builder.Register(c => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>()
                {
                    new DefaultJsonConverter<CustomerOrder>(),
                    new Models.OdataMapper.Order(),
                    new DefaultJsonConverter<OrderItem>()
                }
            });
            return builder.Build();
        }
    }
}
