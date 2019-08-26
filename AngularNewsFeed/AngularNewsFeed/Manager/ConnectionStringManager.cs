using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AngularNewsFeed.Manager
{
    public class ConnectionStringManager
    {
        internal static string getConnectionString()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["bejournalEntities"].ConnectionString;
            if (connectionString.ToLower().StartsWith("metadata="))
            {
                System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder efBuilder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(connectionString);
                connectionString = efBuilder.ProviderConnectionString;
            }
            return connectionString;
        }
    }
}