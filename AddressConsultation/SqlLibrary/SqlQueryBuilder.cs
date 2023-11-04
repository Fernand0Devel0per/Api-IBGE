using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary
{
    public class SqlQueryBuilder
    {
        private StringBuilder _query;

        internal SqlQueryBuilder()
        {
            _query = new StringBuilder();
        }

        public SqlQueryBuilder Select(params string[] columns)
        {
            _query.Append("SELECT ");
            _query.Append(string.Join(", ", columns));
            return this;
        }

        public SqlQueryBuilder From(string tableName)
        {
            _query.Append(" FROM ");
            _query.Append(tableName);
            return this;
        }

        public SqlQueryBuilder Where(string condition)
        {
            _query.Append(" WHERE ");
            _query.Append(condition);
            return this;
        }

        public SqlQueryBuilder And(string condition)
        {
            _query.Append(" AND ");
            _query.Append(condition);
            return this;
        }

        public SqlQueryBuilder Or(string condition)
        {
            _query.Append(" OR ");
            _query.Append(condition);
            return this;
        }

        public SqlQueryBuilder GroupBy(params string[] columns)
        {
            _query.Append(" GROUP BY ");
            _query.Append(string.Join(", ", columns));
            return this;
        }

        public SqlQueryBuilder OrderBy(params string[] columns)
        {
            _query.Append(" ORDER BY ");
            _query.Append(string.Join(", ", columns));
            return this;
        }

        public SqlQueryBuilder Offset(int offset)
        {
            _query.Append(" OFFSET ");
            _query.Append(offset);
            _query.Append(" ROWS");
            return this;
        }

        public SqlQueryBuilder FetchNext(int rowCount)
        {
            _query.Append(" FETCH NEXT ");
            _query.Append(rowCount);
            _query.Append(" ROWS ONLY");
            return this;
        }

        public SqlQueryBuilder InsertInto(string tableName, params string[] columns)
        {
            _query.Append("INSERT INTO ");
            _query.Append(tableName);
            _query.Append(" (");
            _query.Append(string.Join(", ", columns));
            _query.Append(")");
            return this;
        }

        public SqlQueryBuilder Values(params string[] values)
        {
            _query.Append(" VALUES (");
            _query.Append(string.Join(", ", values.Select(v => v.StartsWith("@") ? v : $"'{v}'")));
            _query.Append(")");
            return this;
        }

        public SqlQueryBuilder Update(string tableName)
        {
            _query.Append("UPDATE ");
            _query.Append(tableName);
            return this;
        }

        public SqlQueryBuilder Set(params string[] assignments)
        {
            _query.Append(" SET ");
            _query.Append(string.Join(", ", assignments));
            return this;
        }

        public SqlQueryBuilder DeleteFrom(string tableName)
        {
            _query.Append("DELETE FROM ");
            _query.Append(tableName);
            return this;
        }
        public SqlQueryBuilder InnerJoin(string tableName, string onCondition)
        {
            _query.Append(" INNER JOIN ");
            _query.Append(tableName);
            _query.Append(" ON ");
            _query.Append(onCondition);
            return this;
        }

        public SqlQueryBuilder LeftJoin(string tableName, string onCondition)
        {
            _query.Append(" LEFT JOIN ");
            _query.Append(tableName);
            _query.Append(" ON ");
            _query.Append(onCondition);
            return this;
        }

        public SqlQueryBuilder RightJoin(string tableName, string onCondition)
        {
            _query.Append(" RIGHT JOIN ");
            _query.Append(tableName);
            _query.Append(" ON ");
            _query.Append(onCondition);
            return this;
        }

        public SqlQueryBuilder FullOuterJoin(string tableName, string onCondition)
        {
            _query.Append(" FULL OUTER JOIN ");
            _query.Append(tableName);
            _query.Append(" ON ");
            _query.Append(onCondition);
            return this;
        }

        public string Build()
        {
            string query = _query.ToString();
            _query.Clear();
            return query;
        }

        public void Reset()
        {
            _query.Clear();
        }
    }
}
