using UnityEngine;

namespace GameOne.Assets.Scripts.Utils
{
	public static class VectorUtil
	{
		private const double EPSILON = float.Epsilon;
		private const double EPSILON_SQR = EPSILON * EPSILON;
		private const double EPSILON_VIEWING_VECTOR = 0.000000000000001f;
		public static bool FuzzyEquals0(this float v, double epsilon = EPSILON) => FuzzyEquals(v, 0, epsilon);
		public static bool FuzzyEquals(this float v, float b, double epsilon = EPSILON) => Mathf.Abs(v - b) < epsilon;
		public static bool FuzzyEquals(this Vector2 a, Vector2 b, double epsilon = EPSILON_SQR) => FuzzyEquals0(Vector2.SqrMagnitude(a - b), epsilon);
		public static bool FuzzyEquals0(this Vector2 a, double epsilon = EPSILON_SQR) => FuzzyEquals0(Vector2.SqrMagnitude(a), epsilon);
		public static bool FuzzyEquals(this Vector3 a, Vector3 b, double epsilon = EPSILON_SQR) => FuzzyEquals0(Vector3.SqrMagnitude(a - b), epsilon);
		public static bool FuzzyEquals0(this Vector3 a, double epsilon = EPSILON_SQR) => FuzzyEquals0(Vector3.SqrMagnitude(a), epsilon);

		public static bool FuzzyEquals(this Vector3 a, Vector3 b, int decimals)
		{
			return (int)(a.x * decimals) == (int)(b.x * decimals) &&
				(int)(a.y * decimals) == (int)(b.y * decimals) &&
				(int)(a.z * decimals) == (int)(b.z * decimals);
		}
		/// <summary>
		/// Use to filter out Vector3's that wouldn't cause an issue when used with <see cref="Quaternion.LookRotation(Vector3)"/>
		/// </summary>
		public static bool IsValidViewingVector(this Vector3 a) => !FuzzyEquals0(Vector3.SqrMagnitude(a), EPSILON_VIEWING_VECTOR);
	}
}
