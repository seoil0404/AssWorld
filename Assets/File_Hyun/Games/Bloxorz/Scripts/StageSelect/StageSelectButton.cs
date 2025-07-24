using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Bloxorz.StageSelect
{
    [RequireComponent(typeof(Button))]
    public class StageSelectButton : MonoBehaviour
    {
        [SerializeField] private int stageIndex;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Bloxorz.Core.GameManager.Instance.SelectStage(stageIndex);
            SceneManager.LoadScene("GameScene");
        }
    }
}