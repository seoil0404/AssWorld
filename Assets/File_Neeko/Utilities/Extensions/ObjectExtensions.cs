using System;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UObject = UnityEngine.Object;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class ObjectExtensions {

	public static void Destroy(this UObject target, float time = 0f) => UObject.Destroy(target, time);
	public static void DestroyImmediate(this UObject target, bool allowDestroyingAssets = false) => UObject.DestroyImmediate(target, allowDestroyingAssets);

	public static void DontDestroyOnLoad(this UObject target) => UObject.DontDestroyOnLoad(target);

	public static UObject Instantiate(this UObject original) => UObject.Instantiate(original);
	public static UObject Instantiate(this UObject original, Transform parent) => UObject.Instantiate(original, parent);
	public static UObject Instantiate(this UObject original, Transform parent, bool initializeInWorldSpace) => UObject.Instantiate(original, parent, initializeInWorldSpace);
	public static UObject Instantiate(this UObject original, Scene scene) => UObject.Instantiate(original, scene);
	public static UObject Instantiate(this UObject original, Vector3 position, Quaternion rotation) => UObject.Instantiate(original, position, rotation);
	public static UObject Instantiate(this UObject original, Vector3 position, Quaternion rotation, Transform parent) => UObject.Instantiate(original, position, rotation, parent);
	public static T Instantiate<T>(this T original) where T : UObject => UObject.Instantiate(original);
	public static T Instantiate<T>(this T original, InstantiateParameters parameters) where T : UObject => UObject.Instantiate(original, parameters);
	public static T Instantiate<T>(this T original, Transform parent) where T : UObject => UObject.Instantiate(original, parent);
	public static T Instantiate<T>(this T original, Transform parent, bool worldPositionStays) where T : UObject => UObject.Instantiate(original, parent, worldPositionStays);
	public static T Instantiate<T>(this T original, Vector3 position, Quaternion rotation) where T : UObject => UObject.Instantiate(original, position, rotation);
	public static T Instantiate<T>(this T original, Vector3 position, Quaternion rotation, InstantiateParameters parameters) where T : UObject => UObject.Instantiate(original, position, rotation, parameters);
	public static T Instantiate<T>(this T original, Vector3 position, Quaternion rotation, Transform parent) where T : UObject => UObject.Instantiate(original, position, rotation, parent);
	
	public static void InstantiateAsync<T>(this UObject original) => UObject.InstantiateAsync(original);
	public static void InstantiateAsync<T>(this UObject original, int count) => UObject.InstantiateAsync(original, count);
	public static void InstantiateAsync<T>(this UObject original, Transform parent) => UObject.InstantiateAsync(original, parent);
	public static void InstantiateAsync<T>(this UObject original, InstantiateParameters parameters, CancellationToken cancellationToken = default) => UObject.InstantiateAsync(original, parameters, cancellationToken);
	public static void InstantiateAsync<T>(this UObject original, int count, Transform parent) => UObject.InstantiateAsync(original, count, parent);
	public static void InstantiateAsync<T>(this UObject original, Vector3 position, Quaternion rotation) => UObject.InstantiateAsync(original, position, rotation);
	public static void InstantiateAsync<T>(this UObject original, int count, InstantiateParameters parameters, CancellationToken cancellationToken = default) => UObject.InstantiateAsync(original, count, parameters, cancellationToken);
	public static void InstantiateAsync<T>(this UObject original, int count, Vector3 position, Quaternion rotation) => UObject.InstantiateAsync(original, count, position, rotation);
	public static void InstantiateAsync<T>(this UObject original, int count, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations) => UObject.InstantiateAsync(original, count, positions, rotations);
	public static void InstantiateAsync<T>(this UObject original, Transform parent, Vector3 position, Quaternion rotation) => UObject.InstantiateAsync(original, parent, position, rotation);
	public static void InstantiateAsync<T>(this UObject original, int count, Transform parent, Vector3 position, Quaternion rotation) => UObject.InstantiateAsync(original, count, parent, position, rotation);
	public static void InstantiateAsync<T>(this UObject original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations) => UObject.InstantiateAsync(original, count, parent, positions, rotations);
	public static void InstantiateAsync<T>(this UObject original, Vector3 position, Quaternion rotation, InstantiateParameters parameters, CancellationToken cancellationToken = default) => UObject.InstantiateAsync(original, position, rotation, parameters, cancellationToken);
	public static void InstantiateAsync<T>(this UObject original, int count, Transform parent, Vector3 position, Quaternion rotation, CancellationToken cancellationToken) => UObject.InstantiateAsync(original, count, parent, position, rotation, cancellationToken);
	public static void InstantiateAsync<T>(this UObject original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, CancellationToken cancellationToken) => UObject.InstantiateAsync(original, count, parent, positions, rotations, cancellationToken);
	public static void InstantiateAsync<T>(this UObject original, int count, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, InstantiateParameters parameters, CancellationToken cancellationToken = default) => UObject.InstantiateAsync(original, count, positions, rotations, parameters, cancellationToken);

}