namespace Loans.Domain.Applications
{
    public interface ICreditScorer
    {
        int Score { get; }

        void CalculateScore(string applicantName, string applicantAddress);

        ScoreResult ScoreResult { get; }
<<<<<<< HEAD
<<<<<<< HEAD

        int Count { get; set; }
=======
>>>>>>> a4e42c5 (Solution added)
=======

        int Count { get; set; }
>>>>>>> 32cf57b (Added property tracking)
    }
}