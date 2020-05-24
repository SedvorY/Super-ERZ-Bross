using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;


    public float jumpForce = 5f;
    public float runningSpeed = 1.5f;

    public Animator animator;
    private Rigidbody2D rigidbody;

    private Vector3 startPosition; 


    private void Awake()
    {
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;//Tomamos el valor de inicio de nuestro pj en esta variable
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isMoving", false);
        this.transform.position = startPosition;//Cada vez que reiniciamos, ponemos el pj en la posición de inicio

    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.sharedInstance.currentGameState == GameState.inGame)//solo saltamos si estamos en el estado inGame
        {
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.W)|| Input.GetMouseButtonDown(0))
            { //Se pulsa el espacio
                Jump();
            }
        }
        animator.SetBool("isMoving", Moving()); //animator de moverse a idle
        animator.SetBool("isGrounded", IsTouchingTheGround()); //animator de salto

    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)//Solo nos movemos si estamos en el estado inGame
        {

            if (Input.GetKey(KeyCode.D))
            {
                if (rigidbody.velocity.x < runningSpeed)
                {
                    rigidbody.velocity = new Vector2(runningSpeed, rigidbody.velocity.y); //asignamos un vector de fuerza en horizontal manteniendo el eje veritcal para no cortar saltos
                }

            }
            if (Input.GetKey(KeyCode.A))
            {
                if (rigidbody.velocity.x > -runningSpeed)
                {
                    rigidbody.velocity = new Vector2(-runningSpeed, rigidbody.velocity.y); //asignamos un vector de fuerza en horizontal manteniendo el eje veritcal para no cortar saltos
                }
            }
        }




    }


    void Jump(){
        if (IsTouchingTheGround())
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    //Esta variable sirve para detectar la capa del suelo
    public LayerMask groundLayer;

    bool IsTouchingTheGround() { // true si tocamos el suelo
    
        if(Physics2D.Raycast(this.transform.position,       //trazamos un rayo desde la posición del jugador
                                            Vector2.down,   //en dirección hacia abajo
                                            0.2f,           //hasta un máximo de 20cm
                                            groundLayer     //y nos encontramos con la capa del suelo
                                            )) 
            return true;

            else
                return false;
    }

    bool Moving() {
        if (rigidbody.velocity.x == 0)
            return false;

        else
            return true;
    }
    

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive", false);
    }

}




