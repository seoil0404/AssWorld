using System.Collections;
using UnityEngine;

namespace Bloxorz.Game.Block
{
    public class BlockController : MonoBehaviour
    {
        public enum BlockState { Standing, Lying_X, Lying_Z }

        [SerializeField] private BlockState currentState = BlockState.Standing;
        private bool isMoving = false;

        private static readonly Vector3Int[] Directions = {
            new(0, 0, 1),   // Forward
            new(0, 0, -1),  // Back
            new(-1, 0, 0),  // Left
            new(1, 0, 0)    // Right
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
            StartCoroutine(Roll(dir));
        }

        private IEnumerator Roll(int dir)
        {
            isMoving = true;

            Vector3Int direction = Directions[dir];

            Vector3 pivot = CalculatePivot(direction);
            Vector3 axis = Vector3.Cross(Vector3.up, direction);

            float duration = 0.2f;
            float elapsed = 0f;
            float angle = 90f;

            Quaternion fromRot = transform.rotation;
            Quaternion toRot = Quaternion.AngleAxis(angle, axis) * fromRot;

            Vector3 fromPos = transform.position;
            Vector3 toPos = RotateAroundPoint(fromPos, pivot, axis, angle);

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                transform.SetPositionAndRotation(Vector3.Lerp(fromPos, toPos, t), Quaternion.Slerp(fromRot, toRot, t));
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.SetPositionAndRotation(toPos, toRot);
            UpdateStateAfterMove(dir);
            isMoving = false;
        }

        private Vector3 CalculatePivot(Vector3Int dir)
        {
            Vector3 pos = transform.position;

            switch (currentState)
            {
                case BlockState.Standing:
                    return pos + new Vector3(dir.x, -1, dir.z) * 0.5f;

                case BlockState.Lying_X:
                    if (dir.z != 0) // Z방향 회전
                        return pos + new Vector3(0, -0.5f, dir.z * 0.5f);
                    else // X방향 → 세움
                        return pos + new Vector3(dir.x * 0.5f, -0.5f, 0);

                case BlockState.Lying_Z:
                    if (dir.x != 0)
                        return pos + new Vector3(dir.x * 0.5f, -0.5f, 0);
                    else
                        return pos + new Vector3(0, -0.5f, dir.z * 0.5f);

                default:
                    return pos;
            }
        }

        private Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Vector3 axis, float angle)
        {
            return Quaternion.AngleAxis(angle, axis) * (point - pivot) + pivot;
        }

        private void UpdateStateAfterMove(int dir)
        {
            switch (currentState)
            {
                case BlockState.Standing:
                    currentState = (dir <= 1) ? BlockState.Lying_Z : BlockState.Lying_X;
                    break;
                case BlockState.Lying_X:
                    currentState = (dir >= 2) ? BlockState.Standing : BlockState.Lying_X;
                    break;
                case BlockState.Lying_Z:
                    currentState = (dir <= 1) ? BlockState.Standing : BlockState.Lying_Z;
                    break;
            }
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
    }
}