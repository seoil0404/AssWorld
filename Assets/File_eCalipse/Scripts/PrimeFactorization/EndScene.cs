using UnityEngine;
using UnityEngine.UI;

namespace Ecalipse
{
    namespace PrimeFactorization
    {
        public class EndScene : MonoBehaviour
        {
            private void Start()
            {
                gameObject.GetComponent<Text>().text = GameManager.Instance.GetTime().ToString();
                Debug.Log(GameManager.Instance.GetTime().ToString());

                GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(GameManager.Instance.QuitGame);
            }
        }
    }
}
