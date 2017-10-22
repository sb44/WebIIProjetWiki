using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Wiki.Models.DAL {
    public class WikiConfiguration : DbConfiguration {

        public WikiConfiguration() {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }

    }
}