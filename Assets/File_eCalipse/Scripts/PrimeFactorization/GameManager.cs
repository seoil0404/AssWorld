using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ecalipse
{
    namespace PrimeFactorization
    {
        public class GameManager : MonoBehaviour
        {
            List<int> inputNumbers;
            int targetNumber;

            float time = 0;
            int currentRound = 1;

            Action run;
            bool isRun = false;

            public int timeLimit;
            public int rounds;


            private void Start()
            {
                run += Judge;
            }

            public void Input(int num) => inputNumbers.Add(num);
            public void Run() => run.Invoke();

            IEnumerator Game()
            {
                ScoreManager.Instance.InitScore();

                while (currentRound <= rounds)
                {
                    yield return new WaitUntil(() => isRun);

                    yield return new WaitForSeconds(0.1f);
                }
            }

            void Judge()
            {
                isRun = true;

                List<int> CalculatedTarget = PrimeFactor.Instance.Calculation(targetNumber);

                if (PrimeFactor.Instance.IsCorrect(CalculatedTarget, inputNumbers))
                    ScoreManager.Instance.AddScore();
                ScoreManager.Instance.ShowScore();
            }

            
        }
    }
}
