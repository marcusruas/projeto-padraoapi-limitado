using Microsoft.Extensions.DependencyInjection;
using Repositorio.ConexaoBanco;
using Repositorio.Implementacao;
using Repositorio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.IoC {
    public class IoCRepositorio {
        public static void ConfigurarRepositorio(IServiceCollection services) {
            services.AddSingleton<IConexaoBanco, ConexaoBanco>();
            services.AddScoped<IStatusAplicacaoRepositorio, StatusAplicacaoRepositorio>();
        }
    }
}
