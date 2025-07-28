using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ecalipse
{
    namespace PrimeFactorization
    {
        public class PrimeFactor : MonoBehaviour
        {
            private static PrimeFactor instance;
            public static PrimeFactor Instance
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


            private void Awake()
            {
                instance = this;
            }

            public List<int> Calculation(int number)
            {
                List<int> result = new();

                if (number <= 2)
                {
                    result.Add(number);
                    return result;
                }

                for (int i = 2; number != 1; i++)
                {
                    if (number % i == 0)
                    {
                        result.Add(i);
                        number /= i;
                        i--;
                    }
                }

                return result;
            }

            public bool IsCorrect(List<int> right, List<int> input)
            {
                input.Sort();
                if (right.SequenceEqual(input)) return true;
                else return false;
            }

            public void Print(List<int> list)
            {
                string temp = string.Empty;

                for (int i = 0; i < list.Count; i++)
                {
                    temp += list[i].ToString();
                    if (i != list.Count - 1)
                        temp += " * ";
                }

                Debug.Log(temp);
            }
        }
    }
}

