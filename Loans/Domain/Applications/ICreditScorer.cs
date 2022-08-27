namespace Loans.Domain.Applications
{
    public interface ICreditScorer
    {
        int Score { get; }

        void CalculateScore(string applicantName, string applicantAddress);

        ScoreResult ScoreResult { get; }
<<<<<<< HEAD

        int Count { get; set; }
=======
>>>>>>> a4e42c5 (Solution added)
    }
}