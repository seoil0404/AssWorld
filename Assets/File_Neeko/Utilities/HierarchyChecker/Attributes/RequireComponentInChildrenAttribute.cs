using System;
using System.Linq;
using UnityEngine;

namespace Neeko {

	[AttributeUsage(
		AttributeTargets.Class,
		AllowMultiple = true,
		Inherited = true)
	]

	public class RequireComponentInChildrenAttribute : HierarchyCheckerAttribute {

		public Type ComponentType { get; private set; }
		public string ChildName { get; private set; } = null;

		public RequireComponentInChildrenAttribute(Type componentType) {
			ComponentType = componentType;
		}

		public RequireComponentInChildrenAttribute(Type componentType, string childName) {
			ComponentType = componentType;
			ChildName = childName;
		}

		public override void Check(Component component) {

			bool valid = component
				.GetComponentsInChildren(ComponentType)
				.When(
					ChildName is not null,
					child => child.name == ChildName
				)
				.Any();

			if (valid) return;

			if (ChildName is not null) {
				LogHierarchyError(
					$"Component {component.GetType().Name} in '{component.name}' " +
					$"must have a '{ComponentType.Name}' component in child named '{ChildName}'."
				);
			}

			LogHierarchyError(
				$"Component {component.GetType().Name} in '{component.name}' " +
				$"must have a '{ComponentType.Name}' component in children."
			);

		}

	}
}
