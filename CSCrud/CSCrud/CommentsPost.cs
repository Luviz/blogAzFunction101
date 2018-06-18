
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace CSCrud {
    public static class CommentsPost {
        [FunctionName("Comments")]
        [return: Table("Comments", "AzureWebJobsStorage")]
        public static Comment Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req,
            TraceWriter log) {

            log.Info("C# HTTP trigger Post Comment.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<Comment>(requestBody);

            // thorw if the input is incorrect
            if (data == null)
                throw new InvalidDataException("The Incoming Items dose not match a comment");

            // Set the Entity Values
            data.PartitionKey = "Comment";
            data.RowKey = System.Guid.NewGuid().ToString();

            // Push to table
            return data;
        }
    }
}
