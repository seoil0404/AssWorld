using UnityEngine;
using Wata.Extension.Scene;
using Wata.MapGenerator;

namespace File_wata.Scripts.UI {
    public class TitleScene: MonoBehaviour {

       //==================================================||Fields 
        [SerializeField]
        private StageTypeFrequencyDataTable _stageTypeFrequencyTable;
        
       //==================================================||Methods 
        public void GameStart() {
            
            StageTypeFrequency.LoadFrequency(_stageTypeFrequencyTable);
            SceneManager.LoadScene(Scene.Map);
        }
    }
}