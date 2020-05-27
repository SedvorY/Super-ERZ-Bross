using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GameState { menu, inGame, gameOver} //Posibles estados del videojuego


public class GameManager : MonoBehaviour
{

    //Variable que referencia al propio Game Manager
    public static GameManager sharedInstance;

    public GameState currentGameState = GameState.menu;//Variable para saber en qué estado del juego nos encontramos, la inicialzamos en menú principal


    private void Awake()
    {
        sharedInstance = this;
    }
    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {
             BackToMenu();
        }

        if (Input.GetButtonDown("Start") && currentGameState!= GameState.inGame)
        {
            StartGame();
        }
        
    }


    //Método encargado de iniciar el juego
    public void StartGame() {
        SetGameState(GameState.inGame);

        GameObject camera =GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();
        Debug.Log("Hacemos StartGame en el GameManager");

        if (PlayerController.sharedInstance.transform.position.x > 0)
        {
            Debug.Log("Se está destruyendo y generando");
            LevelGenerator.sharedInstance.RemoveAllTheBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }

        PlayerController.sharedInstance.StartGame();

    }



    //Método que se llamará cuando el jugador muera
    public void GameOver() {
        SetGameState(GameState.gameOver);
    
    }




    //Método para volver al menú inicial cuando el usuario lo quiera hacer
    public void BackToMenu() {
        SetGameState(GameState.menu);
    }


    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            //Preparar la escena de Unity para el menú
        }
        else if (newGameState == GameState.gameOver){
            //Preparar la escena de Unity para el game over
        }
        else if(newGameState == GameState.inGame) { 
        
        }

        this.currentGameState= newGameState;
    }



}
