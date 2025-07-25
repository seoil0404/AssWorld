using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WhenExtensions {

		public static T When<T>(this T target, bool flag, Action<T> action) {

			if (flag) {
				action(target);
			}

			return target;

		}

		public static T When<T>(this T target, bool flag, Func<T, T> selector) {
	
			if (flag) {
				return selector(target);
			}
			return target;

		}

		public static IEnumerable<T> When<T>(this IEnumerable<T> target, bool flag, Func<T, T> selector) {
		
			if (flag) {
				return target.Select(selector);
			}

			return target;

		}

		public static IEnumerable<T> When<T>(this IEnumerable<T> target, bool flag, Func<T, bool> selector) {
		
			if (flag) {
				return target.Where(selector);
			}

			return target;

		}

	}
}
