using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Range (0, 100)]
    private int starsNb = 7;
    [SerializeField]
    [Range (0, 10)]
    private int planetsNb = 7;

    public GameObject holePrefab;
    public GameObject playerPrefab;
    public GameObject starPrefab;
    public GameObject planetPrefab;

    private GameObject[] stars;
    private GameObject[] planets;
    private GameObject player;
    private GameObject hole;

    const int MOUSE = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateUniverse();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject star in stars)
        {
            star.GetComponent<CircularOrbitMove>().MoveObjectAround(hole);
        }

        foreach (GameObject planet in planets)
        {
            planet.GetComponent<CircularOrbitMove>().MoveObjectAround(hole);
        }

        if(Input.GetMouseButton(MOUSE))
            {
                player.GetComponent<PlayerMove>().SetTargetPosition();
            }
        if(player.GetComponent<PlayerMove>().isMoving)
            {
                player.GetComponent<PlayerMove>().MoveObject();
            }
        if(!player.GetComponent<PlayerMove>().isMoving)
            {
                player.GetComponent<CircularOrbitMove>().MoveObjectAround(FindClosestObjectTo(player));
            }
            Debug.DrawLine(player.transform.position,player.transform.position+player.transform.right*100f,Color.green);
            Debug.DrawLine(player.transform.position,player.transform.position+player.transform.up*100f,Color.blue);

    }

    private void GenerateUniverse()
    {
        //Generate Hole
        hole = (GameObject) Instantiate(holePrefab, new Vector3 (0f, 0f, 0f), Quaternion.identity);

        //Generate Stars
        for (int i = 0; i < starsNb; i++)
        {
          float starTheta = Random.Range(0f, 2 * Mathf.PI);
          float starDist = Random.Range(10f, 70f);
          GameObject star = (GameObject)Instantiate(starPrefab, new Vector3( starDist * Mathf.Cos(starTheta), 0f, starDist * Mathf.Sin(starTheta)), Quaternion.identity);
          float scaleAdd = Random.Range(-0.2f, 0.2f);
          star.transform.localScale += new Vector3 (scaleAdd, scaleAdd, scaleAdd);
          //star.GetComponent<CircularOrbitMove>().orbitCenter = hole;
          star.GetComponent<CircularOrbitMove>().speed = Random.Range(1f, 10f);
          //star.GetComponent<CircularOrbitMove>().SetTargetPosition();
          Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
          star.GetComponent<Renderer>().material.SetColor("_EmissionColor", randomColor * 100f);
        }

        stars = GameObject.FindGameObjectsWithTag("Star");

        //Generate Planets
        for (int i = 0; i < planetsNb; i++)
        {
          float planetTheta = Random.Range(0f, 2 * Mathf.PI);
          float planetDist = Random.Range(15f, 80f);
          GameObject planet = (GameObject)Instantiate(planetPrefab, new Vector3( planetDist * Mathf.Cos(planetTheta), 0f, planetDist * Mathf.Sin(planetTheta)), Quaternion.identity);
          Color randomColor = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F));
          planet.GetComponent<Renderer>().material.SetColor("_Color", randomColor);
          float scaleAdd = Random.Range(-1f, 1f);
          planet.transform.localScale += new Vector3 (scaleAdd, scaleAdd, scaleAdd);
          //planet.GetComponent<CircularOrbitMove>().orbitCenter = hole;
          planet.GetComponent<CircularOrbitMove>().speed = Random.Range(1f, 10f);
          //planet.GetComponent<CircularOrbitMove>().SetTargetPosition();
        }

        planets = GameObject.FindGameObjectsWithTag("Planet");

        //Generate Player
        player = (GameObject)Instantiate(playerPrefab, new Vector3(0f, 0f, -70f), Quaternion.identity);
        //player.GetComponent<PlayerMove>().targetPos = player.transform.position;
        player.GetComponent<PlayerMove>().isMoving = false;
    }

    private GameObject FindClosestObjectTo(GameObject sourceObj)
    {
        GameObject result = hole;
        float dist = Vector3.Distance(hole.transform.position, sourceObj.transform.position);
        float distTest = dist;
        foreach(GameObject planet in planets)
        {
          distTest = Vector3.Distance(planet.transform.position, sourceObj.transform.position);
          if (dist > distTest)
          {
            dist = distTest;
            result = planet;
          }
        }
        foreach(GameObject star in stars)
        {
          distTest = Vector3.Distance(star.transform.position, sourceObj.transform.position);
          if (dist > distTest)
          {
            dist = distTest;
            result = star;
          }
        }
        return result;
    }
}
