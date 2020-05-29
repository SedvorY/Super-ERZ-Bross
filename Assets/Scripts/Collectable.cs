using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    bool isCollected = false;

    public int value = 0;

    //Método para activar la moneda y su collider
    void Show() 
    {
        //activamos la imagen de la moneda-->de rebote también la animación
        this.GetComponent<SpriteRenderer>().enabled = true;
        //activa el collider de la moneda para ser recogida
        this.GetComponent<CircleCollider2D>().enabled = true;
        isCollected = false;
    }


    //Método para desactivar la moneda y su collider
    void Hide()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
    }


    //Método para recolectar la moneda
    void Collect()
    {
        isCollected = true;
        Hide();
        GameManager.sharedInstance.CollectObject(value);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag =="Player")
        { 
            Collect();
        }
    }

}
