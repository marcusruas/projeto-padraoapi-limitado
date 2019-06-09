using Microsoft.Extensions.DependencyInjection;
using Servico.Implementacao;
using Servico.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.IoC {
    public class IoCServico {
        public static void ConfigurarServico(IServiceCollection services) {
            services.AddScoped<IStatusServico, StatusServico>();
        }
    }
}
