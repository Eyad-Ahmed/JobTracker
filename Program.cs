using JobTracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var applications = new List<JobApplication>
{
    new(1, "Google", "Backend Engineer", "Applied"),
    new(1, "Noon", "SWE", "To Be Submitted"),
    new(1, "Microsoft", "Frontend Engineer", "Received Interview"),
    new(1, "Meta", "Full Stack Developer", "Accepted")
};

app.MapGet("/applications", () => applications);

app.MapPost("/applications", (CreateJobApplicationRequest request) =>
{
    var newApplication = new JobApplication
    (
        Id: applications.Count > 0 ? applications.Max(a => a.Id) + 1 : 1,
        Company: request.Company,
        Role: request.Role,
        Status: request.Status
    );
    applications.Add(newApplication);
    return Results.Created($"/applications/{newApplication.Id}", newApplication);
});

app.Run();