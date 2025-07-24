using System;
using System.ComponentModel;
using System.Reflection;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class IsDefinedExtensions {

		public static bool IsDefined<T> (this ParameterInfo element) where T : Attribute {
			return element.IsDefined(typeof(T));
		}

		public static bool IsDefined<T> (this Assembly element) where T : Attribute {
			return element.IsDefined(typeof(T));
		}

		public static bool IsDefined<T> (this MemberInfo element) where T : Attribute {
			return element.IsDefined(typeof(T));
		}

		public static bool IsDefined<T> (this MemberInfo element, bool inherit) where T : Attribute {
			return element.IsDefined(typeof(T), inherit);
		}

		public static bool IsDefined<T> (this Module element) where T : Attribute {
			return element.IsDefined(typeof(T));
		}

		public static bool IsDefined<T> (this ParameterInfo element, bool inherit) where T : Attribute {
			return element.IsDefined(typeof(T), inherit);
		}

	}
}
