using Microsoft.Extensions.DependencyInjection;

namespace DM.Kernel.Module
{
    public abstract class DMModule : IDMModule
    {
        public void ConfigureService(IServiceCollection serviceCollection)
        {
            throw new System.NotImplementedException();
        }
    }
}