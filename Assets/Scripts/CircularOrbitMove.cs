using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularOrbitMove : MonoBehaviour
{
    [Range (1f,10f)]
    public float speed = 8f;
    public GameObject gravObj;

    private Vector3 gravPos;
    private float theta;

    void Start()
    {
      if (gravObj == null)
        gravObj = GameObject.Find("Void");

      if(gravObj != null)
        gravPos = gravObj.transform.position;

      float dist = Vector3.Distance(gravPos, transform.position);
      float x = transform.position.x;
      float z = transform.position.z;
      theta = Mathf.Sign(Mathf.Asin(z / dist)) * Mathf.Acos(x / dist);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

      float dist = Vector3.Distance(gravPos, transform.position);
      theta = speed * Time.fixedDeltaTime / dist + theta;
      float x_new = dist * Mathf.Cos(theta);
      float z_new = dist * Mathf.Sin(theta);
      Vector3 pos_new = new Vector3(x_new, transform.position.y, z_new);
      transform.position = Vector3.MoveTowards(transform.position, pos_new, speed * Time.fixedDeltaTime);
    }
}
