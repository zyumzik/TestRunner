using System.Collections.Generic;
using FirebaseModule;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class LeaderboardView : UIView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private List<LeaderboardElement> _topResults;
        [SerializeField] private LeaderboardElement _userResults;

        public int TopResultsCount => _topResults.Count;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Hide);
        }

        public void UpdateTopResults(List<UserData> results)
        {
            if (results == null) return;

            int index = 0;
            while (index < results.Count)
            {
                _topResults[index].Show();
                _topResults[index].UpdateElement(results[index].Rank, results[index].Username, results[index].Score);
                
                index++;
            }
        }

        public void UpdateUserResults(UserData userData)
        {
            _userResults.Show();
            _userResults.UpdateElement(userData.Rank, userData.Username, userData.Score);
        }

        public override void Show()
        {
            foreach (var leaderboardElement in _topResults)
            {
                leaderboardElement.Hide();
            }
            
            _userResults.Hide();
            
            base.Show();
        }
    }
}