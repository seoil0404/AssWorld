using System;
using UnityEngine;

namespace Neeko {

	[AttributeUsage(
		AttributeTargets.Class,
		AllowMultiple = false,
		Inherited = true)
	]

	public class RequireComponentInParentAttribute : HierarchyCheckerAttribute {

		public Type ComponentType { get; private set; }

		public RequireComponentInParentAttribute(Type componentType) {
			ComponentType = componentType;
		}

		public override void Check(Component component) {

			bool valid = component.transform
				.GetComponentInParent(ComponentType);

			if (valid) return;

			LogHierarchyError(
				$"Component {component.GetType().Name} in '{component.name}' " +
				$"must have a '{ComponentType.Name}' component in parent."	
			);

		}

	}
}
