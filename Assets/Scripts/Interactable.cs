using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    public enum InteractionType{NONE,Suicide,Speed}
    public InteractionType type;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 8;
    }

    public void Interact(){        
        FindObjectOfType<InteractSystem>().doInteraction(gameObject,this);                
    }

    
}
