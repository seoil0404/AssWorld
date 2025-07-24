using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UComponent = UnityEngine.Component;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public class HierarchyChecker : SingletonBehaviour<HierarchyChecker> {

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeOnLoad() {

			_disableInstantiateLog = true;
			_customInstanceName = "[Hierarchy Checker]";

			_ = Instance;

		}

		private void Start() {
		
			FindObjectsByType<UComponent>(FindObjectsSortMode.None)
				.SelectMany(component => component
					.GetType()
					.GetCustomAttributes<HierarchyCheckerAttribute>()
					.Select(attribute => (
						Component: component,
						Attribute: attribute
					))
				)
				.Where(pair => pair.Attribute is not null)
				.ForEach(pair => pair.Attribute.Check(pair.Component));

		}

	}
}
