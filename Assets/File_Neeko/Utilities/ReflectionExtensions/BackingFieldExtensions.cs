using System.ComponentModel;
using System.Reflection;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BackingFieldExtensions {
	
		public static bool ContainsBackingField(this PropertyInfo propertyInfo) {
			return propertyInfo.GetBackingField() is not null;
		}

		public static FieldInfo GetBackingField(this PropertyInfo propertyInfo) {
		
			BindingFlags bindingFlags = BindingFlags.NonPublic;

			bindingFlags |= propertyInfo.IsStatic()
				? BindingFlags.Static
				: BindingFlags.Instance;

			string backingFieldName = $"<{propertyInfo.Name}>k__BackingField";

			return propertyInfo.DeclaringType
				.GetField(backingFieldName, bindingFlags);

		}

	}
}
