using UnityEngine;
using UnityEngine.UI;

namespace Ecalipse
{
    namespace PrimeFactorization
    {
        public class ScoreManager : MonoBehaviour
        {
            private static ScoreManager instance;
            public static ScoreManager Instance
            {
                get
                {
                    if (instance == null)
                    {
                        return null;
                    }
                    return instance;
                }
            }

            int score;
            public int targetScore;
            public Text scoreText;


            private void Awake()
            {
                instance = this;
            }

            public void InitScore() => score = 0;
            public void AddScore() => score++;
            public void ShowScore() => scoreText.text = $"{score} / {targetScore}";
            public bool GetIsPlaying() => score < targetScore;
        }
    }
}
