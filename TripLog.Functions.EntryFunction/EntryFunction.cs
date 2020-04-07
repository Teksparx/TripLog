using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System; 
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TripLog.Functions.EntryFunction
{
    public static class EntryFunction
    {
        [FunctionName("entry")] 
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("entry", Connection = "AzureWebJobsStorage")] IAsyncCollector<EntryTableEntity> entryTable,
            [Table("entry", Connection = "AzureWebJobsStorage")] CloudTable entryOutTable,
            ILogger log)
        {
            log.LogInformation(req.Method);
            if (req.Method == "GET")
            {
                var query = new TableQuery<EntryTableEntity>();
                var segment = await entryOutTable.ExecuteQuerySegmentedAsync(query, null);
                return (ActionResult)new OkObjectResult(segment.Select(Mappings.ToEntry));
            }
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var entry = JsonConvert.DeserializeObject<EntryTableEntity>(requestBody);

            if (entry != null)
            {
                await entryTable.AddAsync(entry);
                return (ActionResult)new OkObjectResult(entry);
            }
            return new BadRequestObjectResult("Invalid entry request.");
        }
    }
     
    
}
