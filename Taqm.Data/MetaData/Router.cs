namespace Taqm.Data.MetaData
{
    public static class Router
    {
        #region Fields
        private const string root = "Api";
        private const string version = "V1";
        private const string Rule = root + "/" + version + "/";
        private const string SingleRoute = "{id}";
        #endregion

        #region Routing Classes
        public static class PostRouting
        {
            public const string prefix = Rule + "Post/";
            public const string GetById = prefix + "GetById/" + SingleRoute;
        }
        public static class UserRouting
        {
            public const string prefix = Rule + "User/";
            public const string Create = prefix + "Create";
            public const string GetAll = prefix + "GetAll";
            public const string GetById = prefix + "GetById/" + SingleRoute;
            public const string GetByIdIncludingPosts = prefix + "GetByIdIncludingPosts/" + SingleRoute;
            public const string Update = prefix + "Update";
            public const string Delete = prefix + "Delete/" + SingleRoute;
            public const string ChangePassword = prefix + "ChangePassword";
            public const string ForgetPassword = prefix + "ForgetPassword";
        }
        public static class EmailRouting
        {
            public const string prefix = Rule + "Email/";
            public const string SendEmail = prefix + "SendEmail";
        }
        public static class AuthenticationRouting
        {
            public const string prefix = Rule + "Authentication/";
            public const string ConfirmEmail = prefix + "ConfirmEmail";
            public const string ResetPasswordToken = prefix + "ResetPasswordToken";
            public const string ResetPassword = prefix + "ResetPassword";
            public const string SignIn = prefix + "SignIn";
            public const string CheckRefreshToken = prefix + "CheckRefreshToken";
            public const string RevokeToken = prefix + "RevokeToken";
        }
        public static class EmailRouting
        {
            public const string prefix = Rule + "Email/";
            public const string SendEmail = prefix + "SendEmail";
        }
        #endregion


    }
}
