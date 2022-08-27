using System;

namespace Loans.Domain.Applications
{
    public class LoanApplicationProcessor
    {
        private const decimal MinimumSalary = 65_000;
        private const int MinimumAge = 18;
        private const int MinimumCreditScore = 300;

        private readonly IIdentityVerifier _identityVerifier;
        private readonly ICreditScorer _creditScorer;

        public LoanApplicationProcessor(IIdentityVerifier identityVerifier,
                                        ICreditScorer creditScorer)
        {
            _identityVerifier =
                identityVerifier ?? throw new ArgumentNullException(nameof(identityVerifier));

            _creditScorer =
                creditScorer ?? throw new ArgumentNullException(nameof(creditScorer));
        }

        public void Process(LoanApplication application)
        {
            if (application.GetApplicantSalary() < MinimumSalary)
            {
                application.Decline();
                return;
            }

            if (application.GetApplicantAge() < MinimumAge)
            {
                application.Decline();
                return;
            }

            _identityVerifier.Initialize();

            var isValidIdentity = _identityVerifier.Validate(application.GetApplicantName(),
                                                             application.GetApplicantAge(),
                                                             application.GetApplicantAddress());

            if (!isValidIdentity)
            {
                application.Decline();
                return;
            }

            _creditScorer.CalculateScore(application.GetApplicantName(),
                                         application.GetApplicantAddress());

<<<<<<< HEAD
            _creditScorer.Count++;

            if (_creditScorer.ScoreResult.ScoreValue.Score < MinimumCreditScore)
=======
            _creditScorer.CalculateScore(application.GetApplicantName(),
                                         application.GetApplicantAddress());

<<<<<<< HEAD
            if (_creditScorer.Score < MinimumCreditScore)
>>>>>>> a4e42c5 (Solution added)
=======
            _creditScorer.Count++;


            if (_creditScorer.ScoreResult.ScoreValue.Score < MinimumCreditScore)
>>>>>>> 32cf57b (Added property tracking)
            {
                application.Decline();
                return;
            }

            application.Accept();
        }
    }
}