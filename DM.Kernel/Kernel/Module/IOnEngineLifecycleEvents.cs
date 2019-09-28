using System;

namespace DM.Kernel.Module
{
    public interface IOnEnginePreInitialization
    {
        void OnEnginePreInitialization(IServiceProvider serivceProvider);
    }

    public interface IOnEngineInitialization
    {
        void OnEngineInitialization(IServiceProvider serivceProvider);
    }

    public interface IOnEnginePostInitialization
    {
        void OnEnginePostInitialization(IServiceProvider serivceProvider);
    }

    public interface IOnEngineShutdown
    {
        void OnEngineShutdown(IServiceProvider serviceProvider);
    }
}