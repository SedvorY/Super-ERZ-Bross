﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CollectableType
{
    healthPotion, manaPotion, money
}
public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money;
    bool isCollected = false;

    public AudioClip collectSound;


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
        switch (this.type)
        {
            case CollectableType.money:
                GetComponent<AudioSource>().PlayOneShot(this.collectSound);

                GameManager.sharedInstance.CollectObject(value);
                break;

            case CollectableType.healthPotion:
                PlayerController.sharedInstance.CollectHealth(value);
                break;

            case CollectableType.manaPotion:
                PlayerController.sharedInstance.CollectHealth(value);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag =="Player")
        { 
            Collect();
        }
    }

}
