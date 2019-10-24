using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp1.Models.Configuration;

namespace FunctionApp1
{
    public class HttpTrigger
    {
        private readonly AlertingConfiguration _alertingConfiguration;
        private readonly BackupArchivingConfiguration _backupArchivingConfiguration;
        private readonly BackupConfiguration _backupConfiguration;
        private readonly CassandraConfiguration _cassandraConfiguration;
        

        public HttpTrigger(AlertingConfiguration alertingConfiguration,
            BackupArchivingConfiguration backupArchivingConfiguration,
            BackupConfiguration backupConfiguration,
            CassandraConfiguration cassandraConfiguration)
        {
            _alertingConfiguration = alertingConfiguration;
            _backupArchivingConfiguration = backupArchivingConfiguration;
            _backupConfiguration = backupConfiguration;
            _cassandraConfiguration = cassandraConfiguration;
        }

        [FunctionName("ShowConfig")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            return (ActionResult)new OkObjectResult($"Hello! Backuptargetfolder: |{_backupConfiguration.BackupTargetFolder}| Alerting type: |{_alertingConfiguration.Type}| KeepNumberOfArchives: |{_backupArchivingConfiguration.KeepNumberOfArchives}| Cassandra Hostname: |{_cassandraConfiguration.HostName}|");
        }
    }
}
