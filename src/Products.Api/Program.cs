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

var getProducts = async () => await Task.FromResult(new { });
app.MapGet("/products", getProducts);

var getProductById = async (Guid productId) => await Task.FromResult(productId);
app.MapGet("/products/{productId}", getProductById);

app.UseHttpsRedirection();

app.Run();