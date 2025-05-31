namespace FirebaseModule
{
    [System.Serializable]
    public class UserData
    {
        public string UserId;
        public string Username { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }
        
        public UserData() {}

        public UserData(string username, int score, int rank)
        {
            Username = username;
            Score = score;
            Rank = rank;
        }
    }
}