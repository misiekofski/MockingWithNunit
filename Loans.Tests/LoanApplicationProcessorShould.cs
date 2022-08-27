using Loans.Domain.Applications;
using Moq;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanApplicationProcessorShould
    {
        [Test]
        public void DeclineBecauseOfLowSalary()
        {
            // Arrange
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);

            var application = new LoanApplication(1,
                                                  product,
                                                  amount,
                                                  "Janusz Januszowy",
                                                  25,
                                                  "1234, Cośtam Street",
                                                  64_999);
            // Act
            // mockujemy nulle
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                                   mockCreditScorer.Object);

            sut.Process(application);

            // Assert
            Assert.That(application.GetIsAccepted(), Is.False);
        }

        [Test]
        public void AcceptBecauseOfLowSalary()
        {
            // Arrange
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);

            var application = new LoanApplication(1,
                                                  product,
                                                  amount,
                                                  "Grażyna Grażynowa sadsadsadsad ",
                                                  124,
                                                  "1234, Cośtam Street",
                                                  65_000);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>()))
                                .Returns(true);

            //var mockScoreValue = new Mock<ScoreValue>();
            //mockScoreValue.Setup(x => x.Score).Returns(301);

            //var mockScoreResult = new Mock<ScoreResult>();
            //mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);

            //var mockCreditScorer = new Mock<ICreditScorer>();
            //mockCreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);

            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.SetupProperty(x => x.Count);
            
            var mockScoreValue = new Mock<ScoreValue>();
            mockScoreValue.Setup(x => x.Score).Returns(300);

            var mockScoreResult = new Mock<ScoreResult>(); 
            mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);

            mockCreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);
            ///mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            //mockCreditScorer.Setup(x => x.Score).Returns(300);

            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(301);
            mockCreditScorer.SetupAllProperties();

            // Act
            // mockujemy nulle

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                                   mockCreditScorer.Object);

            sut.Process(application);

            // Assert
            Assert.That(application.GetIsAccepted(), Is.True);
            Assert.That(mockCreditScorer.Object.Count, Is.EqualTo(1));
        }
    }
}