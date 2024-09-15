namespace Wrapper.API.Mapping
{
    /// <summary></summary>
    public static class Module
    {
        /// <summary></summary>
        public static void AddAutoMapperProfiles(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(Startup));      
            services.AddAutoMapper(typeof(CashbookBatchProfile));
            services.AddAutoMapper(typeof(APInvoiceBatchProfile));
            services.AddAutoMapper(typeof(GLSetupProfile));
        }
    }
}
