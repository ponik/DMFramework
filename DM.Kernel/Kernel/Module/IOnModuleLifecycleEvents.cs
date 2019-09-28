namespace DM.Kernel.Module
{
    public interface IPreModuleConfigureServices
    {
        void PreConfigureServices(ModuleServiceConfigurationContext context);
    }

    public interface IPostModuleConfigureServices
    {
        void PostConfigureServices(ModuleServiceConfigurationContext context);
    }
}