const string ApiKeyHeaderName = "x-api-key";
var connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? throw new Exception("ConnectionString not provided.");

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var authMiddleware = async (HttpContext context, RequestDelegate next) =>
{
    var key = context.Request.Headers[ApiKeyHeaderName]!;
    if (string.IsNullOrEmpty(key)) return; //Validate
    await next(context);
};
app.Use(authMiddleware);

var getCategories = async () => await Task.FromResult(new { });
app.MapGet("/categories", getCategories);

var getCategoryById = async (Guid categoryId) => await Task.FromResult(categoryId);
app.MapGet("/categories/{categoryId}", getCategoryById);

app.Run();