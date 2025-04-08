using UnityEngine;

public static class TransformExtensions
{
	public static Vector3 Down(this Transform transform) => transform.rotation * Vector3.down;

	public static void Down(this Transform transform, Vector3 value) => transform.rotation = Quaternion.FromToRotation(Vector3.down, value);

	public static Vector3 Left(this Transform transform) => transform.rotation * Vector3.left;

	public static void Left(this Transform transform, Vector3 value) => transform.rotation = Quaternion.FromToRotation(Vector3.left, value);

	public static Vector3 Back(this Transform transform) => transform.rotation * Vector3.back;

	public static void Back(this Transform transform, Vector3 value) => transform.rotation = Quaternion.FromToRotation(Vector3.back, value);

	public static Vector3 Up(this Transform transform) => transform.rotation * Vector3.up;

	public static void Up(this Transform transform, Vector3 value) => transform.rotation = Quaternion.FromToRotation(Vector3.up, value);

	public static Vector3 Right(this Transform transform) => transform.rotation * Vector3.right;

	public static void Right(this Transform transform, Vector3 value) => transform.rotation = Quaternion.FromToRotation(Vector3.right, value);

	public static Vector3 Forward(this Transform transform) => transform.rotation * Vector3.forward;

	public static void Forward(this Transform transform, Vector3 value) => transform.rotation = Quaternion.FromToRotation(Vector3.forward, value);

	public static void LookAt2D(this Transform transform, Vector3 target) => transform.Down(target - transform.position);

	public static void LookAt2D(this Transform transform, Transform target) => transform.Down(target.position - transform.position);
}
