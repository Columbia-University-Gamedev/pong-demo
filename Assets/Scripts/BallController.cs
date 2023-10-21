using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public float speed;
    private Vector3 direction; // this should always be a unit vector

    GameManager gm;

    float radius;

    public int bounceCount = 0;

    public List<float> speedMultipliers = new List<float>();

    float countdown = 0;
    public float maxCountdown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Limit the range of possible angles for the ball to start traveling in
        direction = Random.insideUnitCircle.normalized;

        radius = GetComponent<CircleCollider2D>().bounds.size.x / 2;

        gm = GameObject.FindObjectOfType<GameManager>();
        
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown > 0)
        {
            //TODO: show countdown in UI
            countdown -= Time.deltaTime;
            return;
        }
        
        float currentSpeed = speed;
        if(speedMultipliers.Count != 0)
        {
            currentSpeed *= speedMultipliers[Mathf.Min(bounceCount, speedMultipliers.Count-1)];
        }
        Vector3 nextPosition = transform.position + (direction * currentSpeed * Time.deltaTime);
        bool directionChanged = false;

        // check x direction
        if (nextPosition.x + radius > (gm.width / 2) || nextPosition.x - radius < -1 * gm.width / 2)
        {
            direction = new Vector3(direction.x * -1, direction.y, direction.z);
            directionChanged = true;
        }

        // check y direction 
        if (nextPosition.y + radius > (gm.height / 2) || nextPosition.y - radius < -1 * gm.height / 2)
        {
            if(transform.position.y > 0)
            {
                gm.ScorePoint(false);
            }
            else
            {
                gm.ScorePoint(true);
            }
            
            //TODO: scoring visual effects
            
            ResetBall();
            return;
        }

        if (!directionChanged)
        {
            transform.position = nextPosition;
        } else
        {
            transform.position += (direction * currentSpeed * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bounceable"))
        {
            bounceCount += 1;
            direction = new Vector3(direction.x, -1 * direction.y, direction.z);
        }
    }

    void ResetBall()
    {
        //TODO: Give the ball a new random angle on reset
        bounceCount = 0;
        transform.position = Vector3.zero;
        countdown = maxCountdown;
    }
}