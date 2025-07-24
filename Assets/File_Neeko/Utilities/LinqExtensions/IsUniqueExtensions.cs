using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class IsUniueExtensions {

		public static bool IsUnique<T>(this IEnumerable<T> enumerable) {
			return enumerable.Distinct().Count() == enumerable.Count();
		}

		public static bool IsUnique<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer) {
			return enumerable.Distinct(comparer).Count() == enumerable.Count();
		}

	}
}
