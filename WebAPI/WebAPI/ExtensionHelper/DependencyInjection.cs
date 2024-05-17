using BussinessLayer.UserBL;
using ConfigurationUtilities.Utilities;
using DataLayer.UsersDAL;
using AppServices.UserService;
using DatabaseTransaction.DBContext;
using Models.ResponseModel.UsersResponse;
using ConfigurationUtilities.Generic;
using AppServices.LoginService;

namespace WebAPI.ExtensionHelper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IObjectToJsonConvertor, ObjectToJsonConvertor>();
            services.AddSingleton<IGenericAppServices, GenericAppServices>();

            services.AddSingleton<IUserAppService, UserAppService>();
            services.AddSingleton<IUserBLL, UserBLL>();
            services.AddSingleton<IUserDAL, UserDAL>();
            services.AddSingleton<UserResponse>();

            services.AddSingleton<ILoginAppService, LoginAppService>();

            services.AddSingleton<DatabaseContext>();      

            return services;
        }
    }
}
