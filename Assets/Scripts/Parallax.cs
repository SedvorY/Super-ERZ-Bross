using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float speed = 0.0f;
    private Rigidbody2D rigidbody;

    private Rigidbody2D directionRigidBody; //queremos ver si la velocidad es positiva o negativa para mover el parallax acorde
    private int parallaxDirection;

    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        directionRigidBody = PlayerController.sharedInstance.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {    //queremos ver si la velocidad es positiva o negativa para mover el parallax acorde
        if (directionRigidBody.velocity.x == 0)
        {
            parallaxDirection = 0;
        }
        else if (directionRigidBody.velocity.x > 0)
        {
            parallaxDirection = -1;
        }
        else if (directionRigidBody.velocity.x < 0)
        {
            parallaxDirection = 1;
        }


        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            this.rigidbody.velocity = new Vector2(speed * parallaxDirection, 0);

            float parentPosition = this.transform.parent.transform.position.x;

            Debug.Log(parentPosition);

            if (parentPosition - this.transform.position.x >= 32.7f)
            {
                this.transform.position = new Vector3(parentPosition + 32.7f, this.transform.position.y, this.transform.position.z);
            }
        }


    }
}