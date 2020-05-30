using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;


    public float jumpForce = 750f;
    public float runningSpeed = 14f;

    public Animator animator;
    private Rigidbody2D rigidbody;

    private Vector3 startPosition;

    private int healthPoints, manaPoints;


    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH = 150, MAX_MANA = 25;

    public const int MIN_HEALTH = 10, MIN_MANA = 0;

    public const float MIN_SPEED = 7, HEALTH_TIME_DECREASE = 1f;

    public const float SUPERJUMP_FORCE = 1.5f;

    public const int SUPERJUMP_COST = 3;

    

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
        
        this.healthPoints = INITIAL_HEALTH;
        this.manaPoints = INITIAL_MANA ;

        StartCoroutine("TiredPlayer");//activamos la corutina
    }
    //Co-Rutina que se gobierna a si misma y va ejecutandose de forma automática
    IEnumerator TiredPlayer()
    {
        while (this.healthPoints > MIN_HEALTH)
        {
            this.healthPoints--;
            yield return new WaitForSeconds(HEALTH_TIME_DECREASE);
        }
        yield return null; //Cuando salgamos del while, la corrutina "duerme" un frame
    }



    // Update is called once per frame
    void Update()
    {


        if (GameManager.sharedInstance.currentGameState == GameState.inGame)//solo saltamos si estamos en el estado inGame
        {
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.W)|| Input.GetMouseButtonDown(0))
            { 
                Jump(false);
            }
        }
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetMouseButtonDown(1))
            { 
                Jump(true);
            }
        }
        animator.SetBool("isMoving", Moving()); //animator de moverse a idle
        animator.SetBool("isGrounded", IsTouchingTheGround()); //animator de salto

    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)//Solo nos movemos si estamos en el estado inGame
        {
            float currentSpeeed = ((runningSpeed-MIN_SPEED) * this.healthPoints / 100.0f)+MIN_SPEED;

            if (Input.GetKey(KeyCode.D))
            {
                if (rigidbody.velocity.x < currentSpeeed)
                {
                    rigidbody.velocity = new Vector2(currentSpeeed, rigidbody.velocity.y); //asignamos un vector de fuerza en horizontal manteniendo el eje veritcal para no cortar saltos
                }

            }
            if (Input.GetKey(KeyCode.A))
            {
                if (rigidbody.velocity.x > -currentSpeeed)
                {
                    rigidbody.velocity = new Vector2(-currentSpeeed, rigidbody.velocity.y); //asignamos un vector de fuerza en horizontal manteniendo el eje veritcal para no cortar saltos
                }
            }
        }
    }


    void Jump(bool superJump){
        if (IsTouchingTheGround())
        {
            if (superJump && this.manaPoints >= SUPERJUMP_COST)
            {
                manaPoints -= SUPERJUMP_COST;
                rigidbody.AddForce(Vector2.up * jumpForce*SUPERJUMP_FORCE, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
            }
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

        //En PlayerPrefs se guardan datos entre partidas, se asigna una clave y un valor inicial
        float currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0);

        if (currentMaxScore < this.GetDistance())
        {
            PlayerPrefs.SetFloat("maxscore", this.GetDistance());
        }

        StopCoroutine("TiredPlayer");
    }

    public float GetDistance()
    {
        float travelledDistance = Vector2.Distance(new Vector2(startPosition.x, 0), new Vector2(this.transform.position.x, 0));
        return travelledDistance;

    }

    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        if (this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return this.healthPoints;
    }

    public int GetMana()
    {
        return this.manaPoints;
    }

}




