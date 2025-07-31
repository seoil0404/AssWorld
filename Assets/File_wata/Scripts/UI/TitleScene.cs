using UnityEngine;
using Wata.Extension.Scene;
using Wata.MapGenerator;

namespace Wata.Scripts.UI {
    public class TitleScene: MonoBehaviour {

       //==================================================||Fields 
        [SerializeField]
        private StageTypeFrequencyDataTable _stageTypeFrequencyTable;
        
       //==================================================||Methods 
        public void GameStart() {
            
            StageTypeFrequency.LoadData(_stageTypeFrequencyTable);
            SceneManager.LoadScene(Scene.Map);
        }
    }
}