using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PaddleController : NetworkBehaviour
{
    public float speed = 5.0f;
    GameManager gm;
    float paddleWidth;

    public KeyCode leftKey;
    public KeyCode rightKey;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        paddleWidth = GetComponent<BoxCollider2D>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        if(Input.GetKey(leftKey))
        {
            if(transform.position.x - paddleWidth > -gm.width / 2)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
        if(Input.GetKey(rightKey))
        {
            if(transform.position.x + paddleWidth < gm.width / 2)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}
