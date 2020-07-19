using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularOrbitMove : MonoBehaviour
{
    public bool trigoRot = false;
    [Range (1f,10f)]
    public float speed = 8f;
    [Range (4,32)]
    public int segments = 16;
    public GameObject gravObj;

    private int nextSegment;
    private Vector3 gravPos;
    private Vector3 targetPos;
    private float [] xSegCoords = null;
    private float [] zSegCoords = null;

    void Start()
    {
      if (gravObj == null)
        gravObj = GameObject.Find("Void");

      if(gravObj != null)
        gravPos = gravObj.transform.position;

      float rotationDir = -1;
      if(trigoRot)
        rotationDir = 1;

      xSegCoords = new float[segments];
      zSegCoords = new float[segments];

      float dist = Vector3.Distance(gravPos, transform.position);
      float angle = Vector3.Angle(gravPos - transform.position, transform.right);
      Debug.Log("Angle " + Vector3.Angle(gravPos - transform.position, transform.right));
      float angleOffset = Mathf.Acos(transform.position.x / dist);

      for(int i = 0; i < segments; i++ )
      {
        xSegCoords[i] = dist * Mathf.Cos( rotationDir * i * 2 * Mathf.PI/segments + angle);
        zSegCoords[i] = dist * Mathf.Sin( rotationDir * i * 2 * Mathf.PI/segments + angle);
      }


      nextSegment = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      targetPos.x = xSegCoords[nextSegment];
      targetPos.y = 0;
      targetPos.z = zSegCoords[nextSegment];

      if(transform.position != targetPos){
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
      }
      else
      {
        if(nextSegment == segments - 1)
        {
          nextSegment = 0;
        }
        else
        {
          nextSegment ++;
        }
      }
    }
}
