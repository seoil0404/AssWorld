using System;
using System.ComponentModel;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BranchExtensions {

		public static TResult Branch<T, TResult>(this T target, bool flag, Func<T, TResult> whenTrue, Func<T, TResult> whenFalse) {
			if (flag) {
				return whenTrue(target);
			}
			else {
				return whenFalse(target);
			}
		}

	}
}
