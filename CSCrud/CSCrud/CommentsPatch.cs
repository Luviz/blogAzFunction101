
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net.Http;

namespace CSCrud {
    public static class CommentsPatch {
        [FunctionName("CommentsPatch")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "Comments/{commentID}/")]HttpRequest req, 
            [Table("Comments", Connection = "AzureWebJobsStorage")] CloudTable table,
            [Table("Comments", "Comment", "{commentID}", Connection = "AzureWebJobsStorage")] Comment comment,
            TraceWriter log) {
            log.Info("C# HTTP trigger Updating Comment");

            var body = new StreamReader(req.Body).ReadToEnd();
            Comment c = JsonConvert.DeserializeObject<Comment>(body);

            // Required Parameter for Merge!
            c.PartitionKey = comment.PartitionKey;
            c.RowKey = comment.RowKey;
            c.ETag = comment.ETag;

            var updateOp = TableOperation.Merge(c);
            table.ExecuteAsync(updateOp);

            return new OkResult();
        }
    }
}
