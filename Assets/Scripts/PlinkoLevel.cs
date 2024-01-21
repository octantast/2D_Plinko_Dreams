using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlinkoLevel : MonoBehaviour
{
    public GeneralController general;
    public Transform launchPoint;
    public GameObject ballPrefab;

    public bool isChoosingAngle = false;
    public Vector2 launchDirection;
    public float launchForceMagnitude;

    public int trajectoryPoints;
    public LineRenderer trajectoryRenderer;

    public GameObject ballHolder;
    public Vector3 direction;

  public LayerMask collisionMask;
    public int maxReflections;


    //public void Awake()
    //{
    //    collisionMask = 1 << LayerMask.NameToLayer("Dots");
    //}

    public void rotateHolder()
    {
        Vector3 directionSpecial2;
        directionSpecial2 = direction;
        directionSpecial2.Normalize();
        float angle = Mathf.Atan2(directionSpecial2.y, directionSpecial2.x) * Mathf.Rad2Deg;
        ballHolder.transform.rotation = Quaternion.Slerp(ballHolder.transform.rotation, Quaternion.Euler(0, 0, angle+90), 10 * Time.deltaTime);

    }
    public void DisplayTrajectory()
    {
        Vector3 directionSpecial;
        directionSpecial = direction;
        trajectoryRenderer.positionCount = 0;
        Vector3[] points = new Vector3[trajectoryPoints];
        float timeStep = 1f / trajectoryPoints;
        int reflections = 0; // —четчик отражений
        int lastPointIndex = 0;

        for (int i = 0; i < trajectoryPoints && reflections < maxReflections; i++)
        {
            Vector2 currentPosition = ballHolder.transform.position;
            float time = i / (float)trajectoryPoints;
            float x = currentPosition.x + directionSpecial.x * time;
            float y = currentPosition.y + directionSpecial.y * time - 0.5f * Mathf.Abs(Physics2D.gravity.y) * time * time;
            Vector2 currentPoint = new Vector2(x, y);


            RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentPoint - currentPosition, Vector2.Distance(currentPosition, currentPoint), collisionMask);
            if (hit.collider != null)
            {
                currentPosition = hit.point;

                lastPointIndex = i;
                break;
            }
            else
            {
                currentPosition = currentPoint;
            }

            points[i] = new Vector3(currentPosition.x, currentPosition.y, 0);
        }

        trajectoryRenderer.positionCount = lastPointIndex;
        trajectoryRenderer.SetPositions(points);
    }

    public void LaunchBall()
    {
        //general.push.gameObject.SetActive(true);
        //general.push.Play();
        if (!general.paused)
        {
            GameObject ball = Instantiate(ballPrefab, ballHolder.transform.position, Quaternion.identity);
            general.allballs.Add(ball);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.AddForce(direction, ForceMode2D.Impulse);
        }
    }
}
