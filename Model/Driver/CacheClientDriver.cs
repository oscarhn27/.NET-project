using System;

namespace NHibernate.Driver
{
    /// <summary>
    /// Author: Werner Kolov
    /// </summary>
    public class CacheClientDriver : ReflectionBasedDriver
    {
		public CacheClientDriver() :
			base(
            "InterSystems.Data.IRISClient",
			"InterSystems.Data.IRISClient",
			"InterSystems.Data.IRISClient.IRISConnection",
			"InterSystems.Data.IRISClient.IRISCommand"
			) 
        {
        }

		public override bool UseNamedPrefixInSql
		{
			get { return false; }
		}

		public override bool UseNamedPrefixInParameter
		{
            get { return false; }
		}

		public override string NamedPrefix
		{
			get { throw new InvalidOperationException("This method must never be called."); }
		}
	}        
}