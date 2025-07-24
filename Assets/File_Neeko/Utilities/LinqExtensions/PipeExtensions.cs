using System;
using System.ComponentModel;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PipeExtensions {

		public static T Pipe<T>(this T obj, Action<T> action) {
			action(obj);
			return obj;
		}

		public static TResult Pipe<T, TResult>(this T obj, Func<T, TResult> func) {
			return func(obj);
		}

	}
}
