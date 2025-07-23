using System;
using System.Collections.Generic;
using Wata.Extension.Scene;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Wata.Extension.UI {
    public class LoadingScene: MonoBehaviour {

        [SerializeField] private TMP_Text _tipBox;
        [SerializeField] private CustomSlider _slider;
        [SerializeField] private List<string> _tips;

        private void Awake() {
            Debug.Log("Hello?");
            _tipBox.text = $"Tips: {_tips[Random.Range(0, _tips.Count)]}";
            StartCoroutine(SceneManager.LoadScene());
        }

        private void Update() {
            _slider.Value = SceneManager.Progress;
        }
    }
}