using System.IO;
using UnityEngine;
using Bloxorz.Data;
using Bloxorz.Core;
using Bloxorz.Game.Tile;
using Bloxorz.Game.Block;

namespace Bloxorz.Game
{
    public class StageLoader : MonoBehaviour
    {
        [SerializeField] private string stageFolder = "Stages";

        private void Awake()
        {
            LoadStage(GameManager.Instance.SelectedStageIndex);
        }

        private void LoadStage(int index)
        {
            string path = Path.Combine(stageFolder, $"Stage_{index}");
            TextAsset json = Resources.Load<TextAsset>(path);

            if (json == null)
            {
                Debug.LogError($"Stage JSON not found: {path}");
                return;
            }

            StageData data = JsonUtility.FromJson<StageData>(json.text);
            BuildStage(data);
        }

        private void BuildStage(StageData data)
        {
            for (int y = 0; y < data.height; y++)
            {
                string line = data.map[y];
                for (int x = 0; x < data.width; x++)
                {
                    char tileChar = line[x];
                    Vector3 worldPos = new(x, 0, data.height - y - 1);

                    switch (tileChar)
                    {
                        case '#':
                            TileFactory.CreateTile(worldPos);
                            break;
                        case 'O':
                            TileFactory.CreateGoalTile(worldPos);
                            break;
                        case 'S':
                            TileFactory.CreateTile(worldPos);
                            BlockSpawner.SpawnBlock(data.blockStart, data.width, data.height);
                            break;
                    }
                }
            }
        }
    }
}