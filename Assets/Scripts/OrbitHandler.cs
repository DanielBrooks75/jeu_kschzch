using UnityEngine;

public class OrbitHandler
{
    public static float getAngleFromCoords(float x, float z, float dist)
    {
        return Mathf.Sign(Mathf.Asin(z / dist)) * Mathf.Acos(x / dist);
    }

    public static Vector3 getCoordsFromPolar(float theta, float dist, float y)
    {
        return new Vector3(dist * Mathf.Cos(theta), y, dist * Mathf.Sin(theta));
    }
    
}