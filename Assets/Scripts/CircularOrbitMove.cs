using UnityEngine;

public class CircularOrbitMove : MonoBehaviour
{
    [Range (-10f,10f)]
    public float speed = 8f;
    /*public string orbitCenterName = "Hole";*/
    public float theta;
    
    public GameObject orbitCenter;
    private Vector3 orbitCenterPos;
    private float dist;
    
    public void SetTargetPosition()
    {
        orbitCenter = GameObject.FindGameObjectWithTag("Hole");
        orbitCenterPos = orbitCenter.transform.position;
        dist = Vector3.Distance(orbitCenterPos, transform.position);
        theta = OrbitHandler.getAngleFromCoords(transform.position.x, transform.position.z, dist);
    }

    // Update is called once per frame
    public void MoveObject()
    {
        dist = Vector3.Distance(orbitCenterPos, transform.position);
        theta = speed * Time.fixedDeltaTime / dist + theta;
        Vector3 pos_new = OrbitHandler.getCoordsFromPolar(theta, dist, transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, pos_new, speed * Time.fixedDeltaTime);
    }
}
