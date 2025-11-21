using ST10446806_PROG6212_POEPART1.Models;

namespace ST10446806_PROG6212_POEPART1.Helpers
{//Lecturer claim rules
    public static class ClaimRules
    {
        public const decimal MaxHourlyRate = 1000m; // max rate a lecturer can enter
        public const decimal MaxHoursPerDay = 13m; // max hours a lecturer can enter
        public const int MinDocumentsRequired = 1; // min amount of docs

        public static bool ValidateClaim(Claim claim)
        {
            if (claim.TotalHours > MaxHoursPerDay) return false;
            if (claim.Documents.Count < MinDocumentsRequired) return false;

            decimal hourlyRate = claim.TotalHours == 0 ? 0 : claim.Amount / claim.TotalHours;
            if (hourlyRate > MaxHourlyRate) return false;

            return true;
        }
    }
    public static class CoordinatorClaimRules
    {
        public const decimal MaxTotalAmount = 10000m;
        public const decimal MaxHoursPerDay = 13m;

        public static bool ValidateClaim(Claim claim)
        {
            if (claim.TotalHours > MaxHoursPerDay) return false;
            if (claim.Amount > MaxTotalAmount) return false;
            if (claim.Documents == null || claim.Documents.Count == 0) return false;

            return true;
        }
    }

}