using System;
using System.Collections.Generic;
using System.Data.Common;

namespace NHibernate.Exceptions
{
    /// <summary>
    /// A SQLExceptionConverter implementation specific to Caché SQL,
    /// accounting for its custom integrity constraint violation error codes.
    /// <p>
    /// Author: Jonathan Levinson
    /// <p>
    /// </p>
    /// Ported from Java by Werner Kolov
    /// </p>
    /// </summary>
    public class CacheSQLStateConverter : ISQLExceptionConverter
    {
//        private static readonly ISet<int> sqlGrammarCategories = new HashSet<int>();
        private static readonly ISet<int> dataCategories = new HashSet<int>();
        private static readonly ISet<int> integrityViolationCategories = new HashSet<int>();
        private static readonly ISet<int> connectionCategories = new HashSet<int>();

        private readonly IViolatedConstraintNameExtracter extracter;

        static CacheSQLStateConverter()
        {
            dataCategories.Add(111);

            integrityViolationCategories.Add(119);
            integrityViolationCategories.Add(120);
            integrityViolationCategories.Add(121);
            integrityViolationCategories.Add(122);
            integrityViolationCategories.Add(123);
            integrityViolationCategories.Add(124);
            integrityViolationCategories.Add(125);
            integrityViolationCategories.Add(127);

            connectionCategories.Add(401);
            connectionCategories.Add(402);
            connectionCategories.Add(405);
            connectionCategories.Add(460);
            connectionCategories.Add(10050);
            connectionCategories.Add(10051);
            connectionCategories.Add(10052);
            connectionCategories.Add(10054);
            connectionCategories.Add(10055);
            connectionCategories.Add(10056);
            connectionCategories.Add(10057);
            connectionCategories.Add(10058);
            connectionCategories.Add(10060);
            connectionCategories.Add(10061);
            connectionCategories.Add(10064);
            connectionCategories.Add(10065);
            connectionCategories.Add(10070);
            connectionCategories.Add(10091);
            connectionCategories.Add(10092);
            connectionCategories.Add(10093);
            connectionCategories.Add(11001);
            connectionCategories.Add(11002);
        }

        public CacheSQLStateConverter(IViolatedConstraintNameExtracter extracter)
        {
            this.extracter = extracter;
        }

        #region ISQLExceptionConverter Members

        public Exception Convert(AdoExceptionContextInfo adoExceptionContextInfo)
        {
            DbException sqlException = ADOExceptionHelper.ExtractDbException(adoExceptionContextInfo.SqlException);
            string message = adoExceptionContextInfo.Message;
            string sql = adoExceptionContextInfo.Sql;

            int errorCode = (int)sqlException.GetType().GetProperty("NativeError").GetValue(sqlException, null);

            if (errorCode >= 1 && errorCode <= 90)
            {
                return new SQLGrammarException(message, sqlException, sql);
            }

            if (integrityViolationCategories.Contains(errorCode))
            {
                string constraintName = extracter.ExtractConstraintName(sqlException);
                return new ConstraintViolationException(message, sqlException, sql, constraintName);
            }

            if (connectionCategories.Contains(errorCode))
            {
                return new ADOConnectionException(message, sqlException, sql);
            }

            if (dataCategories.Contains(errorCode))
            {
                return new DataException(message, sqlException, sql);
            }

            return HandledNonSpecificException(sqlException, message, sql);
        }

        #endregion

        /// <summary> Handle an exception not converted to a specific type based on the SQLState. </summary>
        /// <param name="sqlException">The exception to be handled. </param>
        /// <param name="message">An optional message </param>
        /// <param name="sql">Optionally, the sql being performed when the exception occurred. </param>
        /// <returns> The converted exception; should <b>never</b> be null. </returns>
        public static ADOException HandledNonSpecificException(Exception sqlException, string message, string sql)
        {
            return new GenericADOException(message, sqlException, sql);
        }
    }
}