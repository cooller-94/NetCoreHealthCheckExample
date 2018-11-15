using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreHealthCheckExample
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        //just for demonstrate.In real live this should be stored in some secure place
        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=test;Trusted_Connection=True;ConnectRetryCount=0";
        public string Name => "sql";

        public SqlServerHealthCheck()
        {
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                }
                catch (SqlException)
                {
                    return HealthCheckResult.Failed();
                }
            }

            return HealthCheckResult.Passed();
        }
    }
}
