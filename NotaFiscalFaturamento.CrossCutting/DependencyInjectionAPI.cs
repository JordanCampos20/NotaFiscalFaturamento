using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotaFiscalFaturamento.Application.Interfaces;
using NotaFiscalFaturamento.Application.Mappings;
using NotaFiscalFaturamento.Application.Services;
using NotaFiscalFaturamento.Domain.Interfaces;
using NotaFiscalFaturamento.Infrastructure.Context;
using NotaFiscalFaturamento.Infrastructure.Repositories;

namespace NotaFiscalFaturamento.CrossCutting
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONEXAO_BANCO")
                ?? Environment.GetEnvironmentVariable("CONEXAO_BANCO_NOTA_FISCAL_FATURAMENTO");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<INotaRepository, NotaRepository>();
            services.AddScoped<INotaService, NotaService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
