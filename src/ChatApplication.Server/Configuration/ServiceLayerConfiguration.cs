using ChatApplication.Service.Interfaces;
using ChatApplication.Service.Services;

namespace ChatApplication.Service.Configuration
{
    public static class ServiceLayerConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IGroupChatService, GroupChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IGroupMemberService, GroupMemberService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
        }
    }
}
