using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using ResidentialLoanApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Residencial Loan API",
            Description = "APi to simulate calculation",
            Version = "v1",
            Contact = new OpenApiContact()
            {
                Name = "Thiago Holder",
                Url = new Uri("https://github.com/thiagoholder"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        });
});

// Add services to the container.

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoanResidencial API v1");
});
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-PT"),
});

app.UseHttpsRedirection();

app.MapGet("/loan", IResult (decimal loanAmount, int term, int age, decimal downPayment) =>
{
    app.Logger.LogInformation(
            "Receive new request|" +
           $"Loan Amount: {loanAmount}|" +
           $"Term: {term}|" +
           $"Age: {age}" +
           $"Down Payment: {downPayment}");

    var residentialLoan = new ResidentialLoan(loanAmount, term, age, downPayment);

    return TypedResults.Ok(new
    {
        FinancedAmount = $"{residentialLoan.FinancedAmount:C}",
        MonthlyPayment = $"{residentialLoan.MonthlyPayment:C}",
        TotalCost = $"{residentialLoan.TotalCost:C}",
        TotalInterest = $"{residentialLoan.TotalInterest:C}"
    });
});

app.Run();