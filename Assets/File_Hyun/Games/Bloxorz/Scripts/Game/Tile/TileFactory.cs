using UnityEngine;

namespace Bloxorz.Game.Tile
{
    public static class TileFactory
    {
        private static GameObject _tilePrefab;
        private static GameObject _goalTilePrefab;

        private static Transform _tileRoot;

        private static void Init()
        {
            if (_tileRoot == null)
            {
                _tileRoot = new GameObject("Tiles").transform;
            }

            if (_tilePrefab == null)
                _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");

            if (_goalTilePrefab == null)
                _goalTilePrefab = Resources.Load<GameObject>("Prefabs/Tile_Goal");
        }

        public static void CreateTile(Vector3 position)
        {
            Init();
            GameObject tile = Object.Instantiate(_tilePrefab, position, Quaternion.identity, _tileRoot);
            tile.name = $"Tile_{position.x}_{position.z}";
        }

        public static void CreateGoalTile(Vector3 position)
        {
            Init();
            GameObject tile = Object.Instantiate(_goalTilePrefab, position, Quaternion.identity, _tileRoot);
            tile.name = $"GoalTile_{position.x}_{position.z}";
        }
    }
}