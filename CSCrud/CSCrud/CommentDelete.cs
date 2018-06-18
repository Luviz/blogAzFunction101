
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;

namespace CSCrud {
    public static class CommentDelete {
        [FunctionName("CommentDelete")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Comments/{commentID}/")]HttpRequest req,
            [Table("Comments", Connection = "AzureWebJobsStorage")] CloudTable table,
            [Table("Comments", "Comment", "{commentID}", Connection = "AzureWebJobsStorage")] Comment comment,
            TraceWriter log) {

            var removeOp = TableOperation.Delete(comment);
            table.ExecuteAsync(removeOp);

            return (ActionResult)new OkObjectResult($"Hello");
        }
    }
}
