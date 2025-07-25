using UnityEngine;
using UnityEngine.UI;

namespace Bloxorz
{
    public class StageScene : MonoBehaviour
    {
        [SerializeField] private Image targetImage;

        private float hue;

        private void Update()
        {
            hue += Time.deltaTime * 0.2f;
            if (hue > 1f) hue -= 1f;

            Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
            Color blendedColor = Color.Lerp(Color.white, rainbowColor, 0.4f);

            targetImage.color = blendedColor;
        }
    }
}