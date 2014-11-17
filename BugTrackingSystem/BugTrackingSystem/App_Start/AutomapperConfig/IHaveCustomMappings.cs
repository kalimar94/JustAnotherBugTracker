namespace BugTrackingSystem.App_Start.AutomapperConfig
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}