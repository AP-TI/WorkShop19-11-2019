using NHibernate.Cfg;
using System;

namespace WebShop.Data.NHibernate
{
    // https://bartwullems.blogspot.com/2019/03/nhibernatepostgresqlnaming-strategy.html
    public class PostgreSqlNamingStrategy : INamingStrategy
    {
        public string ClassToTableName(string className)
        {
            return DoubleQuote(className);
        }
        public string PropertyToColumnName(string propertyName)
        {
            return DoubleQuote(propertyName);
        }
        public string TableName(string tableName)
        {
            return DoubleQuote(tableName);
        }
        public string ColumnName(string columnName)
        {
            // behaviour for Id seems to be different
            return columnName == "Id" ? DoubleQuote(columnName) : columnName;
            return DoubleQuote(columnName);
        }
        public string PropertyToTableName(string className,
                                          string propertyName)
        {
            return DoubleQuote(propertyName);
        }
        public string LogicalColumnName(string columnName,
                                        string propertyName)
        {
            return String.IsNullOrWhiteSpace(columnName) ?
                DoubleQuote(propertyName) :
                DoubleQuote(columnName);
        }
        private static string DoubleQuote(string raw)
        {
            raw = raw.Replace("`", "");
            return String.Format("\"{0}\"", raw);
        }
    }
}
