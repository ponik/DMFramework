using Microsoft.Extensions.DependencyInjection;

namespace DM.Kernel.Module
{
    public interface IDMModule
    {
        void ConfigureService(IServiceCollection serviceCollection);
    }
}