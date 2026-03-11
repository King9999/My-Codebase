using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Used to calculate the dot product of 2 vectors. Can be Vector2 or Vector3. Can also get the angle
between 2 vectors. */
namespace MMurray.GenericCode
{
	public static class DotProduct
	{
		public static float GetDotProduct(Vector2 a, Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		public static float GetDotProduct(Vector3 a, Vector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public static float GetAngle(Vector2 a, Vector2 b, bool convertToDegrees = false)
		{
			/*first we get the dot product of a and b. Refresher:
			Dot product of a . b = (a1 * b1) + (a2 * b2) */
			float dotProduct = GetDotProduct(a, b);

			//next we get the magnitude of both vectors
			float magnitudeA = Vector2.SqrMagnitude(a);
			float magnitudeB = Vector2.SqrMagnitude(b);

			//next we calculate the inverse cosine to get the angle. The value returned from Mathf.Acos is in radians so 
			//no conversion is performed unless specified.
			float z = convertToDegrees == true ? Mathf.Rad2Deg * Mathf.Acos(dotProduct / (magnitudeA * magnitudeB)) : 
				Mathf.Acos(dotProduct / (magnitudeA * magnitudeB));

			return z;
		}

		public static float GetAngle(Vector3 a, Vector3 b, bool convertToDegrees = false)
		{
			/*first we get the dot product of a and b. Refresher:
			Dot product of a . b = (a1 * b1) + (a2 * b2) + (a3 * b3) */
			float dotProduct = GetDotProduct(a, b);

			//next we get the magnitude of both vectors
			float magnitudeA = Vector2.SqrMagnitude(a);
			float magnitudeB = Vector2.SqrMagnitude(b);

			//next we calculate the inverse cosine to get the angle. The value returned from Mathf.Acos is in radians so 
			//no conversion is performed unless specified.
			float z = convertToDegrees == true ? Mathf.Rad2Deg * Mathf.Acos(dotProduct / (magnitudeA * magnitudeB)) : 
				Mathf.Acos(dotProduct / (magnitudeA * magnitudeB));

			return z;
		}
	}
}
