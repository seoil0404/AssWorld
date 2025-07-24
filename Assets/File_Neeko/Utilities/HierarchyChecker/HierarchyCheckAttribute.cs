using System;
using UnityEngine;

namespace Neeko {

	[AttributeUsage(
		AttributeTargets.Class,
		AllowMultiple = true,
		Inherited = true)
	]

	public abstract class HierarchyCheckerAttribute : Attribute {

		public abstract void Check(Component component);

		protected void LogHierarchyError(string error) {
			throw new HierarchyException(
				$"Hierarchy Error: {error}"
			);
		}

	}
}
