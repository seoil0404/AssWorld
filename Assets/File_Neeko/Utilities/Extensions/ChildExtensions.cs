using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ChildExtensions {

		public static Transform[] GetChildren(this Transform transform) {
		
			List<Transform> childs = new();

			for (int i = 0; i < transform.childCount; i++) {
				childs.Add(transform.GetChild(i));
			}

			return childs.ToArray();

		}

		/// <summary>
		/// Find()�� �ٸ��� ��ü ���� ������Ʈ�� �ƴ� 1�� ��� ������Ʈ�� Ž��
		/// </summary>
		public static Transform GetChild(this Transform transform, string name) {

			if (transform.childCount == 0) return null;

			return transform
				.GetChildren()
				.First(child => child.name == name);

		}

	}

}
