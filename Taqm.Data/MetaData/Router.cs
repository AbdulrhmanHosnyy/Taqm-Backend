﻿namespace Taqm.Data.MetaData
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
            public const string GetById = prefix + "GetById/" + SingleRoute;
            public const string Update = prefix + "Update";
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
        }
        #endregion


    }
}
