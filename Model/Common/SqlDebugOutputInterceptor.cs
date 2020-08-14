using NHibernate;
using NHibernate.SqlCommand;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Common
{
    public class SqlDebugOutputInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Debug.Write("NHibernate: ");
            Debug.WriteLine(sql);

            return base.OnPrepareStatement(sql);
        }
    }
}
