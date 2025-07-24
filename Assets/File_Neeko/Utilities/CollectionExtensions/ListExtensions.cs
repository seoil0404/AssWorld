using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ListExtensions {


		public static void Clone<T>(this List<T> list, IEnumerable<T> target) {
		
			list.Clear();
			list.AddRange(target);

		}

		public static void SortBy<T>(this List<T> list, Func<T, IComparable> selector) {

			var ordered = list.OrderBy(x => selector(x));
			list.Clone(ordered);

		}

		public static void SortByDescending<T>(this List<T> list, Func<T, IComparable> selector) {
		
			var ordered = list.OrderByDescending(x => selector(x));
			list.Clone(ordered);

		}

	}
}
