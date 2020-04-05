using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TripLog.Functions.EntryFunction
{
    public static class EntryFunction
    {
        [FunctionName("entry")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("Entry",Connection = "AzureWebJobsStorage", Take = 50)] IAsyncCollector<Entry> entryOutput,
            ILogger log)
        {
            log.LogInformation(req.Method);
            if (req.Method == "GET")
            {
                return (ActionResult) new OkObjectResult(req);
            }
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var entry = JsonConvert.DeserializeObject<Entry>(requestBody);

            if (entry != null)
            {
                await entryOutput.AddAsync(entry);
                return (ActionResult) new OkObjectResult(entry);
            }
            return new BadRequestObjectResult("Invalid entry request.");
        }
    }

    public class Entry
    {
        public string Id => Guid.NewGuid().ToString("n");
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }
        // Required for Table Storage entities
        public string PartitionKey => "ENTRY";
        public string RowKey => Id;
    }
}
