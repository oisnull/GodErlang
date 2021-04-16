using GodErlang.Entity.Extends;
using System;

namespace GodErlang.Web.Models
{
    public class UserSessionManager
    {
        const string USER_SESSION_IDENTITY = "CURRENT_LOGIN_USER";

        private static bool CheckSessionAvailable()
        {
            if (AppHttpContext.Current.Session.IsAvailable) return true;

            throw new Exception($"GE后台管理系统下的Session服务不可用，Session是存储在redis里面，请检测redis是否已经开户或者redis的相关配置是否正确");
        }

        public static SessionUser GetCurrentUser()
        {
            if (!CheckSessionAvailable()) return null;

            return AppHttpContext.Current.Session.GetObject<SessionUser>(USER_SESSION_IDENTITY);
        }

        public static void SetUser(SessionUser admin)
        {
            if (admin == null) return;

            if (!CheckSessionAvailable()) return;

            AppHttpContext.Current.Session.SetObject(USER_SESSION_IDENTITY, admin);
        }

        public static void ClearUser()
        {
            if (!CheckSessionAvailable()) return;

            AppHttpContext.Current.Session.Remove(USER_SESSION_IDENTITY);
        }
    }
}
