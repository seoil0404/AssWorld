using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sirenix.Utilities;
using UnityEngine;
using Wata.Extension.Test;

namespace Wata.MapGenerator {
    
    public class MapGenrator: MonoBehaviourWrapper {

        [Header("Size")]
        [SerializeField] private float _width;
        [SerializeField] private float _heightInterval;
        
        [Space]
        [SerializeField] private Vector2 _randomNoise;
        
        [Space]
        [Header("Round Info")]
        [SerializeField] private int _roundCount;
        [SerializeField] private List<int> _roundWidthProbabbly;

        private Dictionary<Stage, Sprite> _mapIcons = null;

        [TestFunction(runtimeOnly: true)]
        private void SetUp() {

            _mapIcons ??= Resources
                .LoadAll<Sprite>("StageIcons")
                .ToDictionary(
                    sprite => {
                        var name = Regex.Match(sprite.name, @"(.*)_\d").Groups[1].Value;
                        var stage = Enum.Parse(typeof(Stage), name);
                        return (Stage)stage;
                    },
                    sprite => sprite
                );
            _mapIcons.ForEach(element => Debug.Log($"{element.Key}: {element.Value}"));
        }

    }
}