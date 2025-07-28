using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ecalipse
{
    namespace PrimeFactorization
    {
        public class GameManager : MonoBehaviour
        {
            private static GameManager instance;
            public static GameManager Instance
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

            List<int> inputNumbers = new();
            int targetNumber;

            float time = 0;
            bool isGame = false;
            bool isRun = false;

            [Header("Texts")]
            public Text expression;
            public Text targetNumberText;
            public Text timeText;

            [Header("Image Sprites")]
            public GameObject correctImage;
            public GameObject wrongImage;


            private void Awake()
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            private void Start()
            {
                StartCoroutine(Game());
            }

            private void Update()
            {
                if (isGame)
                {
                    time += Time.deltaTime;
                    timeText.text = Math.Round(time, 1).ToString();
                }
            }

            public void Input(int num)
            {
                if (isRun) return;

                inputNumbers.Add(num);
                ShowExpression(inputNumbers, expression);
            }
            public void Run() => Judge();

            IEnumerator Game()
            {
                isGame = true;
                ScoreManager.Instance.InitScore();
                ScoreManager.Instance.ShowScore();

                while (ScoreManager.Instance.GetIsPlaying())
                {
                    Init();

                    yield return new WaitUntil(() => isRun);

                    yield return new WaitForSeconds(0.5f);
                }

                expression.text = string.Empty;
                targetNumberText.text = string.Empty;
                correctImage.SetActive(false);

                yield return new WaitForSeconds(0.5f);

                isGame = false;
                SceneManager.LoadScene("PF_End");
            }

            void Judge()
            {
                if (isRun) return;
                isRun = true;

                List<int> CalculatedTarget = PrimeFactor.Instance.Calculation(targetNumber);
                PrimeFactor.Instance.DebugPrint(CalculatedTarget);

                ShowExpression(CalculatedTarget, targetNumberText);

                if (PrimeFactor.Instance.IsCorrect(CalculatedTarget, inputNumbers))
                {
                    ScoreManager.Instance.AddScore();
                    correctImage.SetActive(true);
                }
                else
                {
                    wrongImage.SetActive(true);
                }

                ScoreManager.Instance.ShowScore();
            }

            void Init()
            {
                do
                    targetNumber = UnityEngine.Random.Range(4, 41);
                while (PrimeFactor.Instance.Calculation(targetNumber).Count == 1);

                expression.text = "0";
                targetNumberText.text = targetNumber.ToString();

                correctImage.SetActive(false);
                wrongImage.SetActive(false);

                isRun = false;
                inputNumbers = new();
            }

            void ShowExpression(List<int> list, Text target)
            {
                string temp = string.Empty;

                for (int i = 0; i < list.Count; i++)
                {
                    temp += list[i].ToString();
                    if (i != list.Count - 1)
                        temp += "x";
                }

                target.text = temp;
            }


            public double GetTime() => Math.Round(time, 1);

            /// <summary>
            /// 게임에서 나갑니다
            /// </summary>
            public void QuitGame()
            {
                Destroy(gameObject);
            }
        }
    }
}
