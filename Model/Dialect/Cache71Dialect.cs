using System;
using System.Data;
using System.Data.Common;
using NHibernate.Dialect.Function;
using NHibernate.Dialect.Lock;
using NHibernate.Exceptions;
using NHibernate.Id;
using NHibernate.Persister.Entity;
using NHibernate.SqlCommand;
using Environment = NHibernate.Cfg.Environment;

namespace NHibernate.Dialect
{
    /// <summary>
    /// Caché 2007.1 dialect. This class is required in order to use NHibernate with Intersystems Caché SQL.
    /// Compatible with Caché 2007.1.
    /// <p>
    /// Author: Jonathan Levinson
    /// <p>
    /// </p>
    /// Ported from Java by Werner Kolov,  Gang Gao
    /// </p>
    /// </summary>
    public class Cache71Dialect : Dialect
    {
        /// <summary>
        /// Creates new Caché71Dialect instance. Sets up the DbType / Caché type mappings.
        /// </summary>
        public Cache71Dialect()
        {
            CommonRegistration();
            Register71Functions();
        }

        protected void CommonRegistration()
        {
            RegisterColumnType(DbType.AnsiStringFixedLength, 1, "char(1)");
            RegisterColumnType(DbType.Binary, "varbinary($1)");
            RegisterColumnType(DbType.Byte, "tinyint");
            RegisterColumnType(DbType.Boolean, "bit");
            RegisterColumnType(DbType.Currency, "numeric");
            RegisterColumnType(DbType.Date, "date");
            RegisterColumnType(DbType.DateTime, "timestamp");
            RegisterColumnType(DbType.Decimal, "decimal");
            RegisterColumnType(DbType.Double, "double");
            RegisterColumnType(DbType.Guid, "varchar(64)");
            RegisterColumnType(DbType.Int32, "integer");
            RegisterColumnType(DbType.Int64, "BigInt");
            RegisterColumnType(DbType.Single, "float");
            RegisterColumnType(DbType.String, "varchar(4000)");    
            RegisterColumnType(DbType.Time, "time");
            RegisterColumnType(DbType.UInt16, "smallint");

//		DefaultProperties[Environment.USE_STREAMS_FOR_BINARY] = "false";
            DefaultProperties[Environment.BatchSize] = DefaultBatchSize;
//		DefaultProperties[Environment.STATEMENT_BATCH_SIZE, NO_BATCH];

            DefaultProperties[Environment.UseSqlComments] = "false";

            RegisterFunction("abs", new StandardSQLFunction("abs"));
            RegisterFunction("acos", new StandardSQLFunction("acos", NHibernateUtil.Double));
            RegisterFunction("%alphaup", new StandardSQLFunction("%alphaup", NHibernateUtil.String));
            RegisterFunction("ascii", new StandardSQLFunction("ascii", NHibernateUtil.String));
            RegisterFunction("asin", new StandardSQLFunction("asin", NHibernateUtil.Double));
            RegisterFunction("atan", new StandardSQLFunction("atan", NHibernateUtil.Double));
            RegisterFunction("bit_length", new SQLFunctionTemplate(NHibernateUtil.Int32, "($length(?1)*8)"));
            // hibernate impelemnts cast in Dialect.java
            RegisterFunction("ceiling", new StandardSQLFunction("ceiling", NHibernateUtil.Int32));
            RegisterFunction("char", new StandardSQLFunction("char", NHibernateUtil.Character));
            RegisterFunction("character_length", new StandardSQLFunction("character_length", NHibernateUtil.Int32));
            RegisterFunction("char_length", new StandardSQLFunction("char_length", NHibernateUtil.Int32));
            RegisterFunction("cos", new StandardSQLFunction("cos", NHibernateUtil.Double));
            RegisterFunction("cot", new StandardSQLFunction("cot", NHibernateUtil.Double));
            RegisterFunction("coalesce", new VarArgsSQLFunction("coalesce(", ",", ")"));
            RegisterFunction("concat", new VarArgsSQLFunction(NHibernateUtil.String, "", "||", ""));
            RegisterFunction("convert", new ConvertFunction());
            RegisterFunction("curdate", new StandardSQLFunction("curdate", NHibernateUtil.Date));
            RegisterFunction("current_date", new NoArgSQLFunction("current_date", NHibernateUtil.Date, false));
            RegisterFunction("current_time", new NoArgSQLFunction("current_time", NHibernateUtil.Time, false));
            RegisterFunction
                ("current_timestamp", new ConditionalParenthesisFunction("current_timestamp", NHibernateUtil.DateTime));
            RegisterFunction("curtime", new StandardSQLFunction("curtime", NHibernateUtil.Time));
            RegisterFunction("database", new StandardSQLFunction("database", NHibernateUtil.String));
            RegisterFunction("dateadd", new VarArgsSQLFunction(NHibernateUtil.DateTime, "dateadd(", ",", ")"));
            RegisterFunction("datediff", new VarArgsSQLFunction(NHibernateUtil.Int32, "datediff(", ",", ")"));
            RegisterFunction("datename", new VarArgsSQLFunction(NHibernateUtil.String, "datename(", ",", ")"));
            RegisterFunction("datepart", new VarArgsSQLFunction(NHibernateUtil.Int32, "datepart(", ",", ")"));
            RegisterFunction("day", new StandardSQLFunction("day", NHibernateUtil.Int32));
            RegisterFunction("dayname", new StandardSQLFunction("dayname", NHibernateUtil.String));
            RegisterFunction("dayofmonth", new StandardSQLFunction("dayofmonth", NHibernateUtil.Int32));
            RegisterFunction("dayofweek", new StandardSQLFunction("dayofweek", NHibernateUtil.Int32));
            RegisterFunction("dayofyear", new StandardSQLFunction("dayofyear", NHibernateUtil.Int32));
            // is it necessary to register %exact since it can only appear in a where clause?
            RegisterFunction("%exact", new StandardSQLFunction("%exact", NHibernateUtil.String));
            RegisterFunction("exp", new StandardSQLFunction("exp", NHibernateUtil.Double));
            RegisterFunction("%external", new StandardSQLFunction("%external", NHibernateUtil.String));
            RegisterFunction("$extract", new VarArgsSQLFunction(NHibernateUtil.Int32, "$extract(", ",", ")"));
            RegisterFunction("$find", new VarArgsSQLFunction(NHibernateUtil.Int32, "$find(", ",", ")"));
            RegisterFunction("floor", new StandardSQLFunction("floor", NHibernateUtil.Int32));
            RegisterFunction("getdate", new StandardSQLFunction("getdate", NHibernateUtil.DateTime));
            RegisterFunction("hour", new StandardSQLFunction("hour", NHibernateUtil.Int32));
            RegisterFunction("ifnull", new VarArgsSQLFunction("ifnull(", ",", ")"));
            RegisterFunction("%internal", new StandardSQLFunction("%internal"));
            RegisterFunction("isnull", new VarArgsSQLFunction("isnull(", ",", ")"));
            RegisterFunction("isnumeric", new StandardSQLFunction("isnumeric", NHibernateUtil.Int32));
            RegisterFunction("lcase", new StandardSQLFunction("lcase", NHibernateUtil.String));
            RegisterFunction("left", new StandardSQLFunction("left", NHibernateUtil.String));
            RegisterFunction("len", new StandardSQLFunction("len", NHibernateUtil.Int32));
            RegisterFunction("$length", new VarArgsSQLFunction("$length(", ",", ")"));
            // aggregate functions shouldn't be registered, right?
            //RegisterFunction( "list", new StandardSQLFunction("list",NHibernateUtil.String) );
            // stopped on $list
            RegisterFunction("$list", new VarArgsSQLFunction("$list(", ",", ")"));
            RegisterFunction("$listdata", new VarArgsSQLFunction("$listdata(", ",", ")"));
            RegisterFunction("$listfind", new VarArgsSQLFunction("$listfind(", ",", ")"));
            RegisterFunction("$listget", new VarArgsSQLFunction("$listget(", ",", ")"));
            RegisterFunction("$listlength", new StandardSQLFunction("$listlength", NHibernateUtil.Int32));
            RegisterFunction("locate", new StandardSQLFunction("$FIND", NHibernateUtil.Int32));
            RegisterFunction("log", new StandardSQLFunction("log", NHibernateUtil.Double));
            RegisterFunction("log10", new StandardSQLFunction("log", NHibernateUtil.Double));
            RegisterFunction("lower", new StandardSQLFunction("lower"));
            RegisterFunction("ltrim", new StandardSQLFunction("ltrim"));
            RegisterFunction("minute", new StandardSQLFunction("minute", NHibernateUtil.Int32));
            RegisterFunction("mod", new StandardSQLFunction("mod", NHibernateUtil.Double));
            RegisterFunction("month", new StandardSQLFunction("month", NHibernateUtil.Int32));
            RegisterFunction("monthname", new StandardSQLFunction("monthname", NHibernateUtil.String));
            RegisterFunction("now", new StandardSQLFunction("monthname", NHibernateUtil.DateTime));
            RegisterFunction("nullif", new VarArgsSQLFunction("nullif(", ",", ")"));
            RegisterFunction("nvl", new NvlFunction());
            RegisterFunction("%odbcin", new StandardSQLFunction("%odbcin"));
            RegisterFunction("%odbcout", new StandardSQLFunction("%odbcin"));
            RegisterFunction("%pattern", new VarArgsSQLFunction(NHibernateUtil.String, "", "%pattern", ""));
            RegisterFunction("pi", new StandardSQLFunction("pi", NHibernateUtil.Double));
            RegisterFunction("$piece", new VarArgsSQLFunction(NHibernateUtil.String, "$piece(", ",", ")"));
            RegisterFunction("position", new VarArgsSQLFunction(NHibernateUtil.Int32, "position(", " in ", ")"));
            RegisterFunction("power", new VarArgsSQLFunction(NHibernateUtil.String, "power(", ",", ")"));
            RegisterFunction("quarter", new StandardSQLFunction("quarter", NHibernateUtil.UInt32));
            RegisterFunction("repeat", new VarArgsSQLFunction(NHibernateUtil.String, "repeat(", ",", ")"));
            RegisterFunction("replicate", new VarArgsSQLFunction(NHibernateUtil.String, "replicate(", ",", ")"));
            RegisterFunction("right", new StandardSQLFunction("right", NHibernateUtil.String));
            RegisterFunction("round", new VarArgsSQLFunction(NHibernateUtil.Single, "round(", ",", ")"));
            RegisterFunction("rtrim", new StandardSQLFunction("rtrim", NHibernateUtil.String));
            RegisterFunction("second", new StandardSQLFunction("second", NHibernateUtil.Int32));
            RegisterFunction("sign", new StandardSQLFunction("sign", NHibernateUtil.Int32));
            RegisterFunction("sin", new StandardSQLFunction("sin", NHibernateUtil.Double));
            RegisterFunction("space", new StandardSQLFunction("space", NHibernateUtil.String));
            RegisterFunction("%sqlstring", new VarArgsSQLFunction(NHibernateUtil.String, "%sqlstring(", ",", ")"));
            RegisterFunction("%sqlupper", new VarArgsSQLFunction(NHibernateUtil.String, "%sqlupper(", ",", ")"));
            RegisterFunction("sqrt", new StandardSQLFunction("SQRT", NHibernateUtil.Double));
            RegisterFunction("%startswith", new VarArgsSQLFunction(NHibernateUtil.String, "", "%startswith", ""));
            // below is for Cache' that don't have str in 2007.1 there is str and we register str directly
            RegisterFunction("str", new SQLFunctionTemplate(NHibernateUtil.String, "cast(?1 as char varying)"));
            RegisterFunction("string", new VarArgsSQLFunction(NHibernateUtil.String, "string(", ",", ")"));
            // note that %string is deprecated
            RegisterFunction("%string", new VarArgsSQLFunction(NHibernateUtil.String, "%string(", ",", ")"));
            RegisterFunction("substr", new VarArgsSQLFunction(NHibernateUtil.String, "substr(", ",", ")"));
            RegisterFunction("substring", new VarArgsSQLFunction(NHibernateUtil.String, "substring(", ",", ")"));
            RegisterFunction("sysdate", new NoArgSQLFunction("sysdate", NHibernateUtil.DateTime, false));
            RegisterFunction("tan", new StandardSQLFunction("tan", NHibernateUtil.Double));
            RegisterFunction("timestampadd", new StandardSQLFunction("timestampadd", NHibernateUtil.Double));
            RegisterFunction("timestampdiff", new StandardSQLFunction("timestampdiff", NHibernateUtil.Double));
            RegisterFunction("tochar", new VarArgsSQLFunction(NHibernateUtil.String, "tochar(", ",", ")"));
            RegisterFunction("to_char", new VarArgsSQLFunction(NHibernateUtil.String, "to_char(", ",", ")"));
            RegisterFunction("todate", new VarArgsSQLFunction(NHibernateUtil.String, "todate(", ",", ")"));
            RegisterFunction("to_date", new VarArgsSQLFunction(NHibernateUtil.String, "todate(", ",", ")"));
            RegisterFunction("tonumber", new StandardSQLFunction("tonumber"));
            RegisterFunction("to_number", new StandardSQLFunction("tonumber"));
            // TRIM(end_keyword string-expression-1 FROM string-expression-2)
            // use Hibernate implementation "From" is one of the parameters they pass in position ?3
            //RegisterFunction( "trim", new SQLFunctionTemplate(NHibernateUtil.String, "trim(?1 ?2 from ?3)") );
            RegisterFunction("truncate", new StandardSQLFunction("truncate", NHibernateUtil.String));
            RegisterFunction("ucase", new StandardSQLFunction("ucase", NHibernateUtil.String));
            RegisterFunction("upper", new StandardSQLFunction("upper"));
            // %upper is deprecated
            RegisterFunction("%upper", new StandardSQLFunction("%upper"));
            RegisterFunction("user", new StandardSQLFunction("user", NHibernateUtil.String));
            RegisterFunction("week", new StandardSQLFunction("user", NHibernateUtil.Int32));
            RegisterFunction("xmlconcat", new VarArgsSQLFunction(NHibernateUtil.String, "xmlconcat(", ",", ")"));
            RegisterFunction("xmlelement", new VarArgsSQLFunction(NHibernateUtil.String, "xmlelement(", ",", ")"));
            // xmlforest requires a new kind of function constructor
            RegisterFunction("year", new StandardSQLFunction("year", NHibernateUtil.Int32));
        }

        protected void Register71Functions()
        {
            RegisterFunction("str", new VarArgsSQLFunction(NHibernateUtil.String, "str(", ",", ")"));
        }

        public override bool QualifyIndexName
        {
            // Do we need to qualify index names with the schema name?
            get { return false; }
        }

        public override bool SupportsUnique
        {
            // Does this dialect support the UNIQUE column syntax?
            get { return true; }
        }

        /// <summary>
        /// The syntax used to add a foreign key constraint to a table.
        /// </summary>
        public override String GetAddForeignKeyConstraintString(
            String constraintName,
            String[] foreignKey,
            String referencedTable,
            String[] primaryKey,
            bool referencesPrimaryKey)
        {
            // The syntax used to add a foreign key constraint to a table.
            return new SqlStringBuilder(300)
                .Add(" ADD CONSTRAINT ")
                .Add(constraintName)
                .Add(" FOREIGN KEY ")
                .Add(constraintName)
                .Add(" (")
                .Add(string.Join(", ", foreignKey)) // identifier-commalist
                .Add(") REFERENCES ")
                .Add(referencedTable)
                .Add(" (")
                .Add(string.Join(", ", primaryKey)) // identifier-commalist
                .Add(") ")
                .ToString();
        }

        public bool SupportsCheck()
        {
            // Does this dialect support check constraints?
            return false;
        }

        public override bool SupportsTableCheck
        {
            // Does this dialect support check constraints?
            get { return false; }
        }

        public override bool SupportsColumnCheck
        {
            // Does this dialect support check constraints?
            get { return false; }
        }

        public override String AddColumnString
        {
            // The syntax used to add a column to a table
            get { return " add column"; }
        }

        public String GetCascadeConstraintsString()
        {
            // Completely optional cascading drop clause.
            return "";
        }

        public override bool DropConstraints
        {
            // Do we need to drop constraints before dropping tables in this dialect?
            get { return true; }
        }

        public override bool SupportsCascadeDelete
        {
            get { return true; }
        }

        public override bool HasSelfReferentialForeignKeyBug
        {
            get { return true; }
        }

        // temporary table support ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public override bool SupportsTemporaryTables
        {
            get { return true; }
        }

        public override String GenerateTemporaryTableName(String baseTableName)
        {
            String name = base.GenerateTemporaryTableName(baseTableName);
            return name.Length > 25 ? name.Substring(1, 25) : name;
        }

        public override String CreateTemporaryTableString
        {
            get { return "create global temporary table"; }
        }

        public override bool? PerformTemporaryTableDDLInIsolation()
        {
            return false;
        }

        public override String CreateTemporaryTablePostfix
        {
            get { return ""; }
        }

        public override bool DropTemporaryTableAfterUse()
        {
            return true;
        }

        // IDENTITY support ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public override bool SupportsIdentityColumns
        {
            get { return true; }
        }

        public override System.Type NativeIdentifierGeneratorClass
        {
            get { return typeof (IdentityGenerator); }
        }

        public override bool HasDataTypeInIdentityColumn
        {
            // Whether this dialect has an Identity clause added to the data type or a completely seperate identity
            // data type
            get { return true; }
        }

        public override String IdentityColumnString
        {
            // The keyword used to specify an identity column, if identity column key generation is supported.
            get { return "identity"; }
        }

        public override String IdentitySelectString
        {
            get { return "SELECT LAST_IDENTITY() FROM %TSQL_sys.snf"; }
        }

        // SEQUENCE support ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public override bool SupportsSequences
        {
            get { return false; }
        }

        // lock acquisition support ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool SupportsForUpdate()
        {
            // Does this dialect support the FOR UPDATE syntax?
            return false;
        }

        public new bool SupportsForUpdateOf()
        {
            // Does this dialect support FOR UPDATE OF, allowing particular rows to be locked?
            return false;
        }

        public bool SupportsForUpdateNowait()
        {
            // Does this dialect support the Oracle-style FOR UPDATE NOWAIT syntax?
            return false;
        }

        public override bool SupportsOuterJoinForUpdate
        {
            get { return false; }
        }

        public override ILockingStrategy GetLockingStrategy(ILockable lockable, LockMode lockMode)
        {
            if (lockMode.GreaterThan(LockMode.Read))
            {
                return new UpdateLockingStrategy(lockable, lockMode);
            }
            else
            {
                return new SelectLockingStrategy(lockable, lockMode);
            }
        }

        public override bool SupportsLimit
        {
            get { return true; }
        }

        public override bool SupportsLimitOffset
        {
            get { return false; }
        }

        public override bool SupportsVariableLimit
        {
            get { return true; }
        }

        public override bool UseMaxForLimit
        {
            // Does the LIMIT clause take a "maximum" row number instead of a total number of returned rows?
            get { return true; }
        }

        public override SqlString GetLimitString(SqlString queryString, SqlString offset, SqlString limit)
        {
            // This does not support the Cache SQL 'DISTINCT BY (comma-list)' extensions,
            // but this extension is not supported through Hibernate anyway.
            int insertionPoint = queryString.StartsWithCaseInsensitive("select distinct") ? 15 : 6;

            return new SqlStringBuilder(queryString.Length + 8)
                .Add(queryString)
                .Insert(insertionPoint, " TOP ? ")
                .ToSqlString();
        }

        public override int RegisterResultSetOutParameter(DbCommand statement, int col)
        {
            return col;
        }

        public override DbDataReader GetResultSet(DbCommand ps)
        {
            return ps.ExecuteReader();
        }

        // miscellaneous support ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public override String LowercaseFunction
        {
            // The name of the SQL function that transforms a string to lowercase
            get { return "lower"; }
        }

        public override String NullColumnString
        {
            // The keyword used to specify a nullable column.
            get { return " null"; }
        }

        public override JoinFragment CreateOuterJoinFragment()
        {
            // Create an OuterJoinGenerator for this dialect.
            return new CacheJoinFragment();
        }

        public override String NoColumnsInsertString
        {
            // The keyword used to insert a row without specifying
            // any column values
            get { return " default values"; }
        }

        public override ISQLExceptionConverter BuildSQLExceptionConverter()
        {
            return new CacheSQLStateConverter(extracter);
        }

        private static readonly IViolatedConstraintNameExtracter extracter = new Extracter();

        // Overridden informational metadata ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public override bool SupportsEmptyInList
        {
            get { return false; }
        }

        public override bool AreStringComparisonsCaseInsensitive
        {
            get { return true; }
        }

        public override bool SupportsResultSetPositionQueryMethodsOnForwardOnlyCursor
        {
            get { return false; }
        }
    }

    internal class Extracter : TemplatedViolatedConstraintNameExtracter
    {
        ///<summary>Extract the name of the violated constraint from the given SQLException.</summary>
        /// <param name="sqle">The exception that was the result of the constraint violation.</param> 
        /// <returns>The extracted constraint name.</returns>

        public override string ExtractConstraintName(DbException sqle)
        {
            return ExtractUsingTemplate("constraint (", ") violated", sqle.Message);
        }
    }
}