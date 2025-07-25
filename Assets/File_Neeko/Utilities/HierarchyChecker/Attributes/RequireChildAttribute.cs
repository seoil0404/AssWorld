using System;
using UnityEngine;

namespace Neeko {

	[AttributeUsage(
		AttributeTargets.Class,
		AllowMultiple = false,
		Inherited = true)
	]

	public class RequireChildAttribute : HierarchyCheckerAttribute {

		public int? ChildCount { get; private set; } = null;
		public string ChildName { get; private set; } = null;

		public RequireChildAttribute() {
			ChildCount = 1;
		}

		public RequireChildAttribute(int childCount) {
			ChildCount = childCount;
		}

		public RequireChildAttribute(string childName) {
			ChildName = childName;
		}

		public override void Check(Component component) {

			if (ChildCount is not null) {
			
				if (component.transform.childCount >= ChildCount) return;

				LogHierarchyError(
					$"Component {component.GetType().Name} in '{component.name}' " +
					$"must have at least {ChildCount} children."
				);

				return;

			}

			if (component.transform.GetChild(ChildName) != null) return;

			LogHierarchyError(
				$"Component {component.GetType().Name} in '{component.name}' " +
				$"must have a child named '{ChildName}'."
			);

		}

	}
}
