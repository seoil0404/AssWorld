using UnityEngine;
using Bloxorz.Data;

namespace Bloxorz.Game.Block
{
    public static class BlockSpawner
    {
        private static GameObject _blockPrefab;
        private static Transform _blockRoot;

        public static void SpawnBlock(BlockStartData data, int mapWidth, int mapHeight)
        {
            Init();

            Vector3 center = new(data.x, 1.5f, mapHeight - data.y - 1);
            GameObject block = Object.Instantiate(_blockPrefab, center, Quaternion.identity, _blockRoot);
            block.name = "PlayerBlock";

            if (block.TryGetComponent<BlockController>(out var controller))
            {
                controller.SetInitialState(data.state);
            }
        }

        private static void Init()
        {
            if (_blockRoot == null)
                _blockRoot = new GameObject("Blocks").transform;

            if (_blockPrefab == null)
                _blockPrefab = Resources.Load<GameObject>("Prefabs/Block");
        }
    }
}