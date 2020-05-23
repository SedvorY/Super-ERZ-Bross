using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float runningSpeed = 1.5f;

    public Animator animator;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){ //Se pulsa el espacio
            Jump();
        }
        animator.SetBool("isGrounded", IsTouchingTheGround()); //asignamos true o false para cambiar la animación
        
    }

    private void FixedUpdate()
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
    
    
    
}




