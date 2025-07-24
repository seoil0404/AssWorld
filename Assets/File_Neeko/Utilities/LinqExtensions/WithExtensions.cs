using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WithExtensions {

		public static IEnumerable<(T, TResult)> With<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector) {

			foreach (var element in enumerable) {
				yield return (element, selector(element));
			}

		}

	}
}
