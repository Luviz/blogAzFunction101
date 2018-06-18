
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;


namespace CSCrud {
    public static class CommentsGet {
        [FunctionName("CommentsGet")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Comments/")]HttpRequest req,
            [Table("Comments", Connection = "AzureWebJobsStorage")] CloudTable table,
            TraceWriter log) {
            log.Info("C# HTTP trigger Geting ALL the Comments");

            // Return List
            var comments = new System.Collections.Generic.List<Comment>();

            // Query to Select all item this PK Comment
            var tablequery = new TableQuery<Comment>().Where("PartitionKey eq 'Comment'");
            var querySegment = table.ExecuteQuerySegmentedAsync(tablequery, null);
            
            foreach (var c in querySegment.Result)
                comments.Add(c);

            return new OkObjectResult(comments);
        }
    }

    public static class CommentGet {
        [FunctionName("CommentGet")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Comments/{commentID}/")]HttpRequest req,
            [Table("Comments", "Comment", "{commentID}", Connection = "AzureWebJobsStorage")]Comment comment,
            string commentID,
            TraceWriter log) {
            log.Info($"C# HTTP trigger Getting the Comment with Id {commentID}");
            
            return new OkObjectResult(comment);
        }
    }
}
