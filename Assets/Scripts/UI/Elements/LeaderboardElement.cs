using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class LeaderboardElement : UIView
    {
        [SerializeField] private TMP_Text _indexText;
        [SerializeField] private TMP_Text _nicknameText;
        [SerializeField] private TMP_Text _scoreText;

        public void UpdateElement(int index, string nickname, int score)
        {
            _indexText.text = index.ToString();
            _nicknameText.text = nickname;
            _scoreText.text = score.ToString();
        }
    }
}