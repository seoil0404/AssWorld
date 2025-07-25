using UnityEngine;

namespace Bloxorz.Game.Camera
{
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float distance = 9f;
        [SerializeField] private float xSpeed = 300f;
        [SerializeField] private float ySpeed = 160f;
        [SerializeField] private float yMinLimit = 10f;
        [SerializeField] private float yMaxLimit = 80f;
        [SerializeField] private float zoomSpeed = 6f;
        [SerializeField] private float minDistance = 3f;
        [SerializeField] private float maxDistance = 20f;

        private float x = 0f;
        private float y = 45f;

        private void Start()
        {
            x = 45f;
            y = 45f;

            if (target == null)
            {
                GameObject block = GameObject.Find("PlayerBlock");
                if (block == null)
                {
                    Debug.LogError("No PlayerBlock found");
                    return;
                }

                target = block.transform;
            }
        }

        private void LateUpdate()
        {
            if (target == null) return;

            if (Input.GetMouseButton(1)) // 우클릭 드래그
            {
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            }

            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negDistance = new(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.SetPositionAndRotation(position, rotation);
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}