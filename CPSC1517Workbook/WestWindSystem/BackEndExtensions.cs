using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Required namespaces
using WestWindSystem.BLL;
using WestWindSystem.DAL;

namespace WestWindSystem
{
    public static class BackEndExtensions
    {
        public static void WWBackEndDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            //Register all the services that the system provides
            services.AddDbContext<WestWindContext>(options);

            //Register Services classes as transient services
            services.AddTransient<CustomerServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new CustomerServices(context!);
            });

            //Register Services classes as transient services
            services.AddTransient<CategoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new CategoryServices(context!);
            });

            //Register Services classes as transient services
            services.AddTransient<ProductServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new ProductServices(context!);
            });

			//Register Services classes as transient services
			services.AddTransient<SupplierServices>((serviceProvider) =>
			{
				var context = serviceProvider.GetService<WestWindContext>();

				return new SupplierServices(context!);
			});
		}
    }
}
