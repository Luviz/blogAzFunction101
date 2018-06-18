module.exports = function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');

    context.bindings.outTable = [];

    currentSite = context.bindings.inTable.length

    if (req.body && req.body.name) {
        context.bindings.outTable.push({
            PartitionKey: "Test",
            RowKey: currentSite + 1,
            Name: req.body.name
        });
        context.res = {
            // status: 200, /* Defaults to 200 */
            body: "Hello " + req.body.name
        };
    }
    else {
        context.res = {
            status: 400,
            body: "Please pass a name on the request body"
        };
    }
    context.done();
};