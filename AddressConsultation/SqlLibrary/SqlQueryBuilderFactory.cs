namespace SqlLibrary
{
    public static class SqlQueryBuilderFactory
    {
        public static SqlQueryBuilder Create()
        {
            return new SqlQueryBuilder();
        }
    }
}