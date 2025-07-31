using UnityEngine;
using DG.Tweening;

namespace Bloxorz.Game.Block
{
    public class BlockController : MonoBehaviour
    {
        public enum BlockState { Standing, Lying_X, Lying_Z }

        [SerializeField] private BlockState currentState = BlockState.Standing;
        private bool isMoving = false;

        private static readonly Vector3Int[] Directions = {
            new(0, 0, 1),
            new(0, 0, -1),
            new(-1, 0, 0),
            new(1, 0, 0)
        };

        public void SetInitialState(string stateString)
        {
            if (System.Enum.TryParse(stateString, out BlockState parsed))
            {
                currentState = parsed;
                UpdateTransform();
            }
        }

        public void TryMove(int dir)
        {
            if (isMoving) return;
            Move(dir);
        }

        private void Move(int dir)
        {
            isMoving = true;

            Vector3Int direction = Directions[dir];
            Vector3 axis = Vector3.Cross(Vector3.up, direction);

            BlockState nextState = PredictNextState(dir);
            float moveDistance = (currentState == nextState) ? 0f : 0.5f;

            transform.GetPositionAndRotation(out Vector3 fromPos, out Quaternion fromRot);

            Vector3 pivot = fromPos + new Vector3(direction.x, -moveDistance, direction.z);
            Vector3 rotatedPos = Quaternion.AngleAxis(90f, axis) * (fromPos - pivot) + pivot;
            rotatedPos.y = (nextState == BlockState.Standing) ? 1.5f : 1.0f;

            Quaternion toRot = Quaternion.AngleAxis(90f, axis) * fromRot;

            float duration = 0.2f;

            Sequence seq = DOTween.Sequence();
            seq.Join(transform.DOMove(rotatedPos, duration).SetEase(Ease.InOutSine));
            seq.Join(transform.DORotateQuaternion(toRot, duration).SetEase(Ease.InOutSine));
            seq.OnComplete(() =>
            {
                transform.SetPositionAndRotation(rotatedPos, toRot);
                currentState = nextState;
                UpdateTransform();
                isMoving = false;

                // 클리어 및 게임오버 판정
                if (IsClear())
                {
                    Debug.Log("Stage Clear!");
                    // TODO: 클리어 처리
                }
                else if (IsGameOver())
                {
                    Debug.Log("Game Over!");
                    // TODO: 게임 오버 처리
                }
            });
        }

        private BlockState PredictNextState(int dir)
        {
            return currentState switch
            {
                BlockState.Standing => (dir <= 1) ? BlockState.Lying_Z : BlockState.Lying_X,
                BlockState.Lying_X => (dir >= 2) ? BlockState.Standing : BlockState.Lying_X,
                BlockState.Lying_Z => (dir <= 1) ? BlockState.Standing : BlockState.Lying_Z,
                _ => currentState
            };
        }

        private void UpdateTransform()
        {
            switch (currentState)
            {
                case BlockState.Standing:
                    transform.localScale = new Vector3(1, 2, 1);
                    break;
                case BlockState.Lying_X:
                    transform.localScale = new Vector3(2, 1, 1);
                    break;
                case BlockState.Lying_Z:
                    transform.localScale = new Vector3(1, 1, 2);
                    break;
            }
            transform.rotation = Quaternion.identity;
        }

        private bool IsClear()
        {
            if (currentState != BlockState.Standing) return false;

            Vector3Int below = Vector3Int.RoundToInt(transform.position + Vector3.down);
            GameObject tile = GetTileAt(below);
            return tile != null && tile.name.StartsWith("GoalTile");
        }

        private bool IsGameOver()
        {
            foreach (Vector3Int pos in GetBottomPositions())
            {
                GameObject tile = GetTileAt(pos);
                if (tile == null || (!tile.name.StartsWith("Tile") && !tile.name.StartsWith("GoalTile")))
                {
                    return true;
                }
            }
            return false;
        }

        private GameObject GetTileAt(Vector3Int pos)
        {
            string[] names = {
                $"Tile_{pos.x}_{pos.z}",
                $"GoalTile_{pos.x}_{pos.z}"
            };

            foreach (string name in names)
            {
                GameObject found = GameObject.Find(name);
                if (found != null) return found;
            }

            return null;
        }

        private Vector3Int[] GetBottomPositions()
        {
            Vector3Int center = Vector3Int.RoundToInt(transform.position + Vector3.down);
            return currentState switch
            {
                BlockState.Standing => new[] { center },
                BlockState.Lying_X => new[] {
                    center + Vector3Int.left,
                    center + Vector3Int.right
                },
                BlockState.Lying_Z => new[] {
                    center + new Vector3Int(0, 0, -1),
                    center + new Vector3Int(0, 0, 1)
                },
                _ => new[] { center }
            };
        }
    }
}