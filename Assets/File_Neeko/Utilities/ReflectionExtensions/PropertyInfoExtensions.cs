using System.ComponentModel;
using System.Reflection;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PropertyInfoExtensions {

		public static bool IsStatic(this PropertyInfo propertyInfo) {
		
			MethodInfo method =
				propertyInfo.GetGetMethod() ??
				propertyInfo.GetSetMethod();

			if (method is null) {
				return false;
			}

			return method.IsStatic;

		}

	}
}
