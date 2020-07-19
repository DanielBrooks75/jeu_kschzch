using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularOrbitMove : MonoBehaviour
{
    [Range (1f,10f)]
    public float speed = 8f;
    [Range (4,32)]
    public int segments = 16;

    private int nextSegment;
    private Vector3 voidPos;
    private Vector3 targetPos;
    private float [] xSegCoords = null;
    private float [] zSegCoords = null;

    void Start()
    {
      if(GameObject.Find("Void") != null)
        voidPos = GameObject.Find("Void").transform.position;

      xSegCoords = new float[segments];
      zSegCoords = new float[segments];

      float dist = Vector3.Distance(voidPos, transform.position);
      float angleOffset = Mathf.Acos(transform.position.x / dist);

      for(int i = 0; i < segments; i++ )
      {
        xSegCoords[i] = dist * Mathf.Cos(- i * 2 * Mathf.PI/segments + angleOffset);
        zSegCoords[i] = dist * Mathf.Sin(- i * 2 * Mathf.PI/segments + angleOffset);
      }


      nextSegment = 1;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
      //targetPos.x =   this.transform.position.x + Mathf.Tan(22.5f) * speed
      targetPos.x = xSegCoords[nextSegment];
      targetPos.y = 0;
      targetPos.z = zSegCoords[nextSegment];
      Debug.Log("targetPos "+ targetPos );

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
