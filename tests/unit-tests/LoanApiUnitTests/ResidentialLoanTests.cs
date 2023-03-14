namespace LoanApiUnitTests
{
    using ResidentialLoanApi.Models;

    public class ResidentialLoanTests
    {
        [Theory]
        [InlineData(-10000, 30, 25, 0)]
        [InlineData(10000, 0, 25, 0)]
        [InlineData(10000, 30, 16, 0)]
        [InlineData(10000, 30, 25, 0)]
        [InlineData(10000, 30, 25, -5000)]
        public void Constructor_ThrowsArgumentException(decimal loanAmount, int term, int age, decimal downPayment)
        {
            Assert.Throws<ArgumentException>(() => new ResidentialLoan(loanAmount, term, age, downPayment));
        }

        [Fact]
        public void InterestRate_ReturnsCorrectValue_WhenAgeUnderOrEqualTo30()
        {
            // Arrange
            var loan = new ResidentialLoan(200000, 30, 30, 40000);

            // Act
            decimal interestRate = loan.InterestRate;

            // Assert
            Assert.Equal(0.025m, interestRate);
        }

        [Fact]
        public void InterestRate_ReturnsCorrectValue_WhenAgeBetween31And50()
        {
            // Arrange
            var loan = new ResidentialLoan(200000, 30, 35, 50000);

            // Act
            decimal interestRate = loan.InterestRate;

            // Assert
            Assert.Equal(0.03m, interestRate);
        }

        [Fact]
        public void InterestRate_ReturnsCorrectValue_WhenAgeOver50()
        {
            // Arrange
            var loan = new ResidentialLoan(200000, 30, 60, 60000);

            // Act
            decimal interestRate = loan.InterestRate;

            // Assert
            Assert.Equal(0.035m, interestRate);
        }

        [Fact]
        public void FinancedAmount_ReturnsCorrectValue()
        {
            // Arrange
            var loan = new ResidentialLoan(200000, 30, 30, 40000);

            // Act
            decimal financedAmount = loan.FinancedAmount;

            // Assert
            Assert.Equal(160000, financedAmount);
        }

        [Fact]
        public void MonthlyPayment_ReturnsCorrectValue()
        {
            // Arrange
            var loan = new ResidentialLoan(200000, 30, 30, 40000);

            // Act
            var monthlyPayment = loan.MonthlyPayment;

            // Assert
            Assert.Equal(632.19m, monthlyPayment, 2);
        }
    }
}