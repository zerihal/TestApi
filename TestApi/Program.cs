using Microsoft.EntityFrameworkCore;
using Testable.Interfaces;
using TestApi;
using TestApi.Helpers;
using TestApi.Models;
using TestApi.Objects;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TestDb>(opt => opt.UseInMemoryDatabase("TestableList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Prepopulate the DB
TestApiHelper.PrePopDb(app);

// Default root get
app.MapGet("/", () => "Test API for testing contained methods");

// Gets all test object implementations in the DB
app.MapGet("/TestableItems", async (TestDb db) => await db.TestObjectImps.ToListAsync());

// Gets all test object implmentations by the specified name
app.MapGet("/NamedTestableItems/{name}", async (string name, TestDb db) =>
{
    var testObjImps = await db.TestObjectImps.ToListAsync();
    return testObjImps?.Where(t => t.Name == name) is IEnumerable<ITestable> testables && 
        testables.Count() > 0 ? Results.Ok(testables) : Results.NotFound();
});

// Gets test object implementation by specified DB ID
app.MapGet("/TestableItem/{id}", async (int id, TestDb db) =>
{
    return await db.TestObjectImps.FindAsync(id) is ITestable testable
        ? Results.Ok(testable) : Results.NotFound();
});

// Get test object implementation by the TestableId (as string representation of GUID)
app.MapGet("/GuidTestableItem/{testableId}", async (string testableId, TestDb db) =>
{
    if (Guid.TryParse(testableId, out var testableIdGuid))
    {
        var testObjImps = await db.TestObjectImps.ToListAsync();
        return testObjImps.FirstOrDefault(t => t.TestableID == testableIdGuid) is ITestable testable
            ? Results.Ok(testable)
            : Results.NotFound();
    }
    else
    {
        return Results.BadRequest();
    }
});

// Route handlers to execute methods - the below just execute local functions and return the result 
// as a RouteHandlerBuilder (looks like JSON in browser), that could potentially be parsed as JSON
// or used for further action. 
app.MapGet("/SampleStr", SampleMethods.TestStringOut);
app.MapGet("/SampleIntArr", SampleMethods.TestIntArrayOut);
app.MapGet("/SampleObjArr", SampleMethods.TestObjArrayOut);

app.MapGet("/MultiTest", (int id, string method, Arg[] args) =>
{
    // To test this GET ...https://localhost:7286/MultiTest?id=1&method=test&args=3

    // This is to test only as one way to do a GET with some object args. In order to do this
    // we need to use a model that has a TryParse implementation. This will then allow us to
    // bind to the required object, in this case the Arg model is just an object. From here
    // it should be then possible to parse the objects and cast to the appropriate types for
    // the given method.

    // Note: This method is for dev test purposes only, but retained for reference in case of use
    // for something else later.

    var argsStr = string.Empty;

    foreach (var obj in args)
    {
        argsStr += $"{obj},";
    }

    argsStr = argsStr.TrimEnd(',');

    var rtn = $"{id},{method},[{argsStr}]";
    return Results.Text(rtn);
});

// Takes the HttpRequest JSON object and attempts to cast it to a MethodObject type, which is a lightweight
// record with Testable object GUID (as string), method name and object arguments. This will then get the
// Testable object from the DB by ID and attempt to execute the method with the arguments passed, returning
// the object (or null if void) from the method.
app.MapPost("/ExecuteMethod", async (HttpRequest methodRequest, TestDb db) =>
{
    object? rtn = null;
    var methodObj = await methodRequest.ReadFromJsonAsync<MethodObject>();

    if (methodObj != null && Guid.TryParse(methodObj.TestableIdStr, out var testableId))
    {
        var testObjImps = await db.TestObjectImps.ToListAsync();
        if (testObjImps.FirstOrDefault(o => o.TestableID == testableId) is ITestable testable)
        {
            rtn = testable.ExecuteMethod(methodObj.MethodName, methodObj.args, out var err);

            if (rtn == null && err != string.Empty)
                rtn = err;
        }
    }
    else
    {
        // ToDo - Maybe check to see if there is a DLL that should be loaded for this?
    }

    return Results.Ok(rtn);
});

app.MapPost("/UploadTestable", async (IFormFile file, TestDb db) =>
{
    // ToDo - check that the file is a DLL, and if so then put it somewhere so that it can be loaded
    // and any TestableBase implementation loaded into the DB ...

    var testObjImps = await db.TestObjectImps.ToListAsync();

    return Results.Ok();
});

app.Run();
