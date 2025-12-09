using Microsoft.Extensions.DependencyInjection;
using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Application.Mappings;

namespace Library.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Registrar Servicios
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService, LoanService>();

            return services;
        }
    }
}