using System;
using UnityEngine;

namespace Neeko {

	[AttributeUsage(
		AttributeTargets.Class,
		AllowMultiple = false,
		Inherited = true)
	]

	public class RequireParentAttribute : HierarchyCheckerAttribute {

		public override void Check(Component component) {

			if (component.transform.root != component.transform) return;

			LogHierarchyError(
				$"Component {component.GetType().Name} in '{component.name}' " +
				$"must to have a parent."
			);

		}

	}
}
