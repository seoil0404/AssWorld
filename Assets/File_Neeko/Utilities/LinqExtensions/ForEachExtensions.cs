using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ForEachExtensions {

		public static void ForEach(this IEnumerable enumerable, Action<object> action) {
			foreach (var element in enumerable) {
				action(element);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
			foreach (var element in enumerable) {
				action(element);
			}
		}

		public static void ForEach(this IDictionary dictionary, Action<object> action) {
			foreach (var element in dictionary) {
				action(element);
			}
		}
	
		public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<KeyValuePair<TKey, TValue>> action) {
			foreach (var pair in dictionary) {
				action(pair);
			}
		}

		public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action) {
			foreach (var (key, value) in dictionary) {
				action(key, value);
			}
		}

	}
}
