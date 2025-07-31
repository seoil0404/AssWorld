using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

namespace Neeko {

	public class GunMuzzleAnimator : GunShootAnimateTrigger {

		//======================================================================| Inspector Fields

		[SerializeField]
		private float _flareHoldingTime = 0.175f;

		//======================================================================| Fields

		private IEnumerable<VisualEffect> _visualEffects;
		private IEnumerable<(Light2D Light, float InitialIntensity)> _lights;

		//======================================================================| Unity Behaviours

		private void Awake() {
		
			_visualEffects = GetComponentsInChildren<VisualEffect>();
			_lights = GetComponentsInChildren<Light2D>()
				.Select(light => (light, light.intensity))
				.ToList();

		}

		private void Start() {
			foreach (var (light, _) in _lights) {
				light.intensity = 0f;
			}
		}

		//======================================================================| Methods

		public override void OnShoot() {

			foreach (var visualEffect in _visualEffects) {
				visualEffect.Play();
			}

			foreach (var (light, intensity) in _lights) {
				light.intensity = intensity;
				print(light.intensity);
				ReleaseLight(light);
			}

			void ReleaseLight(Light2D light) => DOTween
				.To(
					() => light.intensity,
					(x) => light.intensity = x,
					0f, _flareHoldingTime
				)
				.SetEase(Ease.OutCubic);

		}

	}

}