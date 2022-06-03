using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public static class RepositoryRegister
    {
        public static void MapRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericDapperRepository, GenericDapperRepository>();
        }
    }
}
