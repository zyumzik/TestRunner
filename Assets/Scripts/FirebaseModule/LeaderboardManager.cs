using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

namespace FirebaseModule
{
    public class LeaderboardManager
    {
        private readonly string _leaderboardPathString;
        private readonly AuthManager _authManager;
        private readonly DatabaseReference _dbReference;

        public event Action OnLeaderboardLoadingFailed;
        
        public LeaderboardManager(string leaderboardPathString, AuthManager authManager)
        {
            _leaderboardPathString = leaderboardPathString;
            _authManager = authManager;
            _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public Task SubmitScore(int score)
        {
            var userId = _authManager.UserId;
            var username = _authManager.UserDisplayName;

            var data = new Dictionary<string, object>
            {
                { "Username", username },
                { "Score", score }
            };

            return _dbReference.Child(_leaderboardPathString).Child(userId).SetValueAsync(data);
        }

        public async Task<UserData> GetUserScore()
        {
            var userId = _authManager.UserId;
            //var allSnapshot = await _dbReference.Child(_leaderboardPathString).OrderByValue().GetValueAsync();
            var allSnapshot = await _dbReference.Child(_leaderboardPathString).OrderByChild("Score").GetValueAsync();


            if (allSnapshot == null || !allSnapshot.HasChildren) return null;

            var scoreList = new List<UserData>();

            foreach (var child in allSnapshot.Children)
            {
                var id = child.Key;
                var score = int.Parse(child.Child("Score").Value.ToString());
                var username = child.Child("Username").Value.ToString();

                scoreList.Add(new UserData
                {
                    UserId = id,
                    Username = username,
                    Score = score
                });
            }

            // Сортуємо по Score спадаючи
            scoreList.Sort((a, b) => b.Score.CompareTo(a.Score));

            for (int i = 0; i < scoreList.Count; i++)
            {
                if (scoreList[i].UserId == userId)
                {
                    scoreList[i].Rank = i + 1;
                    return scoreList[i];
                }
            }

            return null;
        }
        
        public Task<List<UserData>> GetTopScores(int count)
        {
            var tcs = new TaskCompletionSource<List<UserData>>();

            _dbReference.Child(_leaderboardPathString).OrderByChild("Score").LimitToLast(count).GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError($"Error getting leaderboard: {task.Exception}");
                        OnLeaderboardLoadingFailed?.Invoke();
                        tcs.SetResult(null);
                        return;
                    }

                    var snapshot = task.Result;
                    var list = new List<UserData>();

                    foreach (var child in snapshot.Children)
                    {
                        var userId = child.Key;
                        var score = Convert.ToInt32(child.Child("Score").Value);
                        var username = child.Child("Username").Value?.ToString();

                        list.Add(new UserData
                        {
                            UserId = userId,
                            Username = username,
                            Score = score
                        });
                    }

                    list.Sort((a, b) => b.Score.CompareTo(a.Score));

                    // Додаємо Rank
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].Rank = i + 1;
                    }

                    tcs.SetResult(list);
                });

            return tcs.Task;
        }
    }
}