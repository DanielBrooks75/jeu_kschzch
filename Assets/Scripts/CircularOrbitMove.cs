using UnityEngine;

public class CircularOrbitMove : MonoBehaviour
{
    [Range (0, 10f)]
    public float speed = 8f;
    /*public string orbitCenterName = "Hole";*/
    //public float theta;

    //public GameObject orbitCenter;

    public bool trigonometricRotation;
    //private Vector3 orbitCenterPos;
    //private float dist;

    // public void SetTargetPosition()
    // {
    //     dist = Vector3.Distance(orbitCenter.transform.position, transform.position);
    //     theta = OrbitHandler.getAngleFromCoords(transform.position.x, transform.position.z, dist);
    // }

    // public void MoveObject()
    // {
    //     dist = Vector3.Distance(orbitCenter.transform.position, transform.position);
    //     theta = speed * Time.fixedDeltaTime / dist + theta;
    //     Vector3 pos_new = OrbitHandler.getCoordsFromPolar(theta, dist, transform.position.y);
    //     transform.position = Vector3.MoveTowards(transform.position, pos_new, speed * Time.fixedDeltaTime);
    // }

    public void MoveObjectAround(GameObject orbitCenter)
    {
        Vector3 dir = orbitCenter.transform.position - transform.position;
        if (Vector3.Angle(transform.right, dir) < 90)
        {
          transform.right = (orbitCenter.transform.position - transform.position).normalized;
        }
        else
        {
          transform.right = - (orbitCenter.transform.position - transform.position).normalized;
        }

        transform.position += (transform.forward  * speed + orbitCenter.GetComponent<Rigidbody>().velocity ) * Time.deltaTime;
    }
}
