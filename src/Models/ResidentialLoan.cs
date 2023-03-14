namespace ResidentialLoanApi.Models
{
    public class ResidentialLoan
    {
        public decimal LoanAmount { get; }
        public int Term { get; }
        public int Age { get; }
        public decimal DownPayment { get; }

        public ResidentialLoan(decimal loanAmount, int term, int age, decimal downPayment)
        {
            if (loanAmount <= 0)
                throw new ArgumentException("Loan amount must be greater than zero.", nameof(loanAmount));

            if (term <= 0)
                throw new ArgumentException("Term must be greater than zero.", nameof(term));

            if (age < 18)
                throw new ArgumentException("Age must be at least 18.", nameof(age));

            if (downPayment < 0)
                throw new ArgumentException("Down payment cannot be negative.", nameof(downPayment));

            LoanAmount = loanAmount;
            Term = term;
            Age = age;
            DownPayment = downPayment;
        }

        public decimal InterestRate
        {
            get
            {
                switch (Age)
                {
                    case <= 30:
                        return 0.025m;

                    case > 30 and <= 50:
                        return 0.03m;

                    default:
                        return 0.035m;
                }
            }
        }

        public decimal FinancedAmount => LoanAmount - DownPayment;

        public decimal MonthlyPayment
        {
            get
            {
                decimal monthlyInterestRate = InterestRate / 12;
                int totalPayments = Term * 12;
                decimal discountedValue = (decimal)Math.Pow(1 + (double)monthlyInterestRate, -totalPayments);
                decimal annuityFactor = (1 - discountedValue) / monthlyInterestRate;

                return FinancedAmount / annuityFactor;
            }
        }

        public decimal TotalCost => MonthlyPayment * Term * 12;

        public decimal TotalInterest => TotalCost - FinancedAmount;
    }
}