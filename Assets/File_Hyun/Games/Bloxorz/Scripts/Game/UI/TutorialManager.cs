using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
            Destroy(gameObject);
    }
}
