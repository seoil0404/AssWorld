using System.Collections.Generic;
using UnityEngine;
using Bloxorz.Game.Block;

namespace Bloxorz.Game.UI
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private float offset = 1.5f;
        [SerializeField] private float height = 1f;

        private Transform target;
        private readonly List<GameObject> arrows = new();

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
            Vector3[] directions = {
                Vector3.forward, // North (Z+)
                Vector3.back,    // South (Z-)
                Vector3.left,    // West (X-)
                Vector3.right    // East (X+)
            };

            for (int i = 0; i < arrows.Count; i++)
            {
                Vector3 pos = center + directions[i] * offset;
                pos.y = center.y + height;
                arrows[i].transform.position = pos;
                arrows[i].transform.LookAt(UnityEngine.Camera.main.transform);
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

                int captured = i; // for closure
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