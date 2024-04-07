namespace Taqm.Data.MetaData
{
    public static class Router
    {
        private const string root = "Api";
        private const string version = "V1";
        private const string Rule = root + "/" + version + "/";
        private const string SingleRoute = "{id}";

        public static class PostRouting
        {
            public const string prefix = Rule + "Post/";
            public const string GetById = prefix + "GetById/" + SingleRoute;
        }

    }
}
