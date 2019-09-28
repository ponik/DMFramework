using Microsoft.Extensions.DependencyInjection;

namespace DM.Kernel.Module
{
    public interface IFnModule
    {
        void ConfigureServices(ModuleServiceConfigurationContext context);
    }
}