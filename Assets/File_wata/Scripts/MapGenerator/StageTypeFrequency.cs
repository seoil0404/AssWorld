using System.Collections.Generic;
using System.Linq;

namespace Wata.MapGenerator {
    public static class StageTypeFrequency {

        //==================================================||Fields
        private static Dictionary<Stage, uint> frequency = new();

       //==================================================||Methods
       public static void LoadFrequency(StageTypeFrequencyDataTable pSetting) {
           frequency = pSetting.Table
               .ToDictionary(
                   element => element.Type,
                   element => (uint)element.Frequency
               );
       }

       public static void AddPercent(Stage pStage, uint pAmount) =>
            frequency[pStage] += pAmount;
        public static void SubPercent(Stage pStage, uint pAmount) =>
                    frequency[pStage] -= pAmount;
        public static void MulPercent(Stage pStage, uint pAmount) =>
                    frequency[pStage] *= pAmount;
        public static void DivPercent(Stage pStage, uint pAmount) =>
                    frequency[pStage] /= pAmount;

        public static Stage Random() => Random(new Stage[] { });
        public static Stage Random(params Stage[] pExclude) {

            var count = frequency.Sum(stage => stage.Value)
                - pExclude.Sum(exclude => frequency[exclude]);

            var random = UnityEngine.Random.Range(0, count);
            var result = Stage.Battle;
            var curIdx = 0u;
            
            foreach (var stage in frequency ) {
                bool isExistDuplication = false;
                foreach (var exclude in pExclude) {
                    if (stage.Key == exclude) {
                        isExistDuplication = true;
                        break;
                    }
                }
                
                if(isExistDuplication)
                    continue;

                curIdx += stage.Value;
                if (curIdx >= random) {
                    result = stage.Key;
                    break;
                }
            }

            return result;
        }
    }
}