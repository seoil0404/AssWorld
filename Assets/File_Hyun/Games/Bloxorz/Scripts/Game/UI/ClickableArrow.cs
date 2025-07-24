using System;
using UnityEngine;

namespace Bloxorz.Game.UI
{
    public class ClickableArrow : MonoBehaviour
    {
        public Action OnClicked;

        private void OnMouseDown()
        {
            OnClicked?.Invoke();
        }
    }
}