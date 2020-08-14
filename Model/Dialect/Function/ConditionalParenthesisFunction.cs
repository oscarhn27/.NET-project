using System.Collections;
using NHibernate.Engine;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace NHibernate.Dialect.Function
{
    /// <summary>
    /// Essentially the same as <see cref="StandardSQLFunction"/>, 
    /// except that here the parentheses are not included when no arguments are given.
    /// <p>
    /// Author: Jonathan Levinson
    /// <p>
    /// </p>
    /// Ported from Java by Werner Kolov
    /// </p>
    /// </summary>
    public class ConditionalParenthesisFunction : StandardSQLFunction
    {

        public ConditionalParenthesisFunction(string name) : base(name) {
        }

        public ConditionalParenthesisFunction(string name, IType type)
            : base(name, type)
        {
        }

        // TODO Not virtual in the base class. Should actually override, but seems not to be used anywhere.
        public new bool HasParenthesesIfNoArguments {
            get { return false; }
        }

        public override SqlString Render(IList args, ISessionFactoryImplementor factory)
        {
            bool hasArgs = args.Count > 0;

            SqlStringBuilder buf = new SqlStringBuilder();
            buf.Add(name);
            if (hasArgs)
            {
                buf.Add("(");
                for (int i = 0; i < args.Count; i++)
                {
                    object arg = args[i];
                    if (arg is Parameter || arg is SqlString)
                    {
                        buf.AddObject(arg);
                    }
                    else
                    {
                        buf.Add(arg.ToString());
                    }
                    if (i < (args.Count - 1)) buf.Add(", ");
                }
                buf.Add(")");
            }
            return buf.ToSqlString();
        }
    }
}
