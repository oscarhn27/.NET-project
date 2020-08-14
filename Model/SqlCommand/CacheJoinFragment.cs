namespace NHibernate.SqlCommand
{
    /// <summary>
    /// A Caché dialect join. Differs from ANSI only in that full outer join is not supported.
    /// <p>
    /// Author: Jeff Miller
    /// </p>
    /// <p>
    /// Author: Jonathan Levinson
    /// </p>
    /// <p>
    /// Ported from Java by Werner Kolov
    /// </p>
    /// </summary>
    public class CacheJoinFragment : ANSIJoinFragment
    {
        public override void AddJoin(string tableName, string alias, string[] fkColumns, string[] pkColumns,
                                     JoinType joinType)
        {
            if (joinType == JoinType.FullJoin)
            {
                throw new AssertionFailure("Cache does not support full outer joins");
            }
            base.AddJoin(tableName, alias, fkColumns, pkColumns, joinType);
        }
    }
}