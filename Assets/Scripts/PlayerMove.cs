using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  [Range (0f, 100f)]
  public float speed = 10f;
  private Vector3 targetPos;
  public bool isMoving;
  //const int MOUSE = 0;
  // Use this for initialization1
  // void Start () {
  //
  //     targetPos = transform.position;
  //     isMoving = false;
  // }

  // Update is called once per frame
  // void FixedUpdate () {
  //
  //     if(Input.GetMouseButton(MOUSE))
  //     {
  //         SetTargetPosition();
  //     }
  //     if(isMoving)
  //     {
  //         MoveObject();
  //     }
  //     if(!isMoving)
  //     {
  //         //TryToOrbit();
  //     }
  // }
  public void SetTargetPosition()
  {
      Plane plane = new Plane(Vector3.up,transform.position);
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      float point = 0f;

      if(plane.Raycast(ray, out point))
          targetPos = ray.GetPoint(point);

      isMoving = true;
  }
  public void MoveObject()
  {

      //Check neighboors
      Collider[] hitColliders = Physics.OverlapSphere(transform.position,15f);
      //Debug.Log("nb of colliders :" + hitColliders.Length);
      float actualSpeed = speed * hitColliders.Length;


      transform.LookAt(targetPos, Vector3.up);
      transform.position = Vector3.MoveTowards(transform.position, targetPos, actualSpeed * Time.fixedDeltaTime);

      if (transform.position == targetPos)
          isMoving = false;
      Debug.DrawLine(transform.position,targetPos,Color.red);

      GameObject hole = GameObject.FindGameObjectsWithTag("Hole")[0];
      // Vector2 holePosAng = new Vector2 (holePos[0], holePos[2]);
      // Debug.Log("PosH :" + holePosAng + " PosP :" + new Vector2 (transform.position.x, transform.position.z) + " frow:" + transform.forward);
      // Debug.Log("hole.right:"+hole.transform.right + " right:" + transform.right);
      // Debug.Log("Angle From Hole in °:" + Vector3.Angle(hole.transform.right, transform.position));

  }
  // void TryToOrbit()
  // {
  //     Collider[] hitColliders = Physics.OverlapSphere(transform.position,10f);
  //     if(hitColliders.Length > 1)
  //     {
  //       GameObject grav = hitColliders[0].gameObject;
  //       if (grav.name != "PlayerNew")
  //       {
  //         Debug.Log("grav " + grav.name);
  //         /*GetComponent<CircularOrbitMove>().orbit_center = grav;*/
  //         //GetComponent<CircularOrbitMove>().enabled = true;
  //       }
  //       //CircularOrbitMove.gravObj = hitColliders[0].GameObject;
  //       //CircularOrbitMove.enabled = true;
  //     }
  // }
}
