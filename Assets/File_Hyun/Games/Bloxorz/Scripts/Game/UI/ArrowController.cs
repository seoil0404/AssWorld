using System.Collections.Generic;
using UnityEngine;
using Bloxorz.Game.Block;

namespace Bloxorz.Game.UI
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private float offset = 1.5f;

        private Transform target;
        private readonly List<GameObject> arrows = new();

        private static readonly Vector3[] Directions =
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        private static readonly Quaternion[] ArrowRotations =
        {
            Quaternion.Euler(90, 0, 0),
            Quaternion.Euler(90, 180, 0),
            Quaternion.Euler(90, -90, 0),
            Quaternion.Euler(90, 90, 0)
        };

        private void Start()
        {
            GameObject block = GameObject.Find("PlayerBlock");
            if (block == null)
            {
                Debug.LogError("No PlayerBlock found");
                return;
            }

            target = block.transform;
            CreateArrows();
        }

        private void Update()
        {
            if (target == null) return;

            Vector3 center = target.position;

            for (int i = 0; i < arrows.Count; i++)
            {
                Vector3 pos = center + Directions[i] * offset;
                pos.y = center.y;

                arrows[i].transform.SetPositionAndRotation(pos, ArrowRotations[i]);
            }
        }

        private void CreateArrows()
        {
            ClearArrows();

            string[] dirNames = { "Up", "Down", "Left", "Right" };

            for (int i = 0; i < 4; i++)
            {
                GameObject arrow = Instantiate(arrowPrefab);
                arrow.name = $"Arrow_{dirNames[i]}";
                arrow.transform.SetParent(transform);

                int captured = i;
                arrow.GetComponent<ClickableArrow>().OnClicked = () =>
                {
                    target.GetComponent<BlockController>().TryMove(captured);
                };

                arrows.Add(arrow);
            }
        }

        private void ClearArrows()
        {
            foreach (var go in arrows)
            {
                if (go) Destroy(go);
            }
            arrows.Clear();
        }
    }
}