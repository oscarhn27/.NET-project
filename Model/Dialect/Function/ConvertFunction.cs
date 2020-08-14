using System;
using System.Collections;
using NHibernate.Engine;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace NHibernate.Dialect.Function
{
    /// <summary>
    /// A Caché defintion of a convert function.
    /// <p>
    /// Author: Jonathan Levinson
    /// <p>
    /// </p>
    /// Ported from Java by Werner Kolov
    /// </p>
    /// </summary>
    public class ConvertFunction : ISQLFunction
    {
        #region ISQLFunction Members

        public IType ReturnType(IType columnType, IMapping mapping)
        {
            return NHibernateUtil.String;
        }

        public SqlString Render(IList args, ISessionFactoryImplementor factory)
        {
            if (args.Count != 2 && args.Count != 3)
            {
                throw new QueryException("convert() requires two or three arguments");
            }
            var type = (String) args[1];

            var buf = new SqlStringBuilder();

            if (args.Count == 2)
            {
                return buf.Add("{fn convert(").AddObject(args[0]).Add(" , ").Add(type).Add(")}").ToSqlString();
            }

            return buf.Add("convert(").AddObject(args[0]).Add(" , ").Add(type).Add(" , ").AddObject(args[2]).Add(")").ToSqlString();
        }

        public bool HasArguments
        {
            get { return true; }
        }

        public bool HasParenthesesIfNoArguments
        {
            get { return true; }
        }

        #endregion
    }
}