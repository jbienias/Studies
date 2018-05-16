namespace CSGO.Helpers
{
    public class RegexPatterns
    {
        public const string nicknamePattern = "^[A-Za-z0-9-._]{3,25}$";
        public const string steamProfilePattern = @"https:\/\/steamcommunity\.com\/id\/[A-Za-z0-9_-]{3,25}$";
        public const string teamNamePattern = "^[A-Za-z-._()\\s]{3,20}$";
    }
}
