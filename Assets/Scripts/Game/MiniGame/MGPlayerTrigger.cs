using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPlayerTrigger : MonoBehaviour
{
    [SerializeField] string tagString;

    private void OnTriggerEnter2D(Collider2D collision)
    {
                
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(tagString))
        {
            collision.gameObject.SetActive(false);

            MGDrop drop = collision.gameObject.GetComponent<MGDrop>();

            if (gameObject.tag.Equals("Obstacle"))
                GameEvents.OnLifeRemove?.Invoke(1, drop.IsLastDrop);
            else
            {
                Audio.PlaySFXMGCollect();
                GameEvents.OnDropCollected?.Invoke(1, drop.IsLastDrop);
            } 
        }  
    }
}
