using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickUp : MonoBehaviour {

    public AbilityItem abilityItem;
    private SpriteRenderer itemSpriteRenderer;

    private void OnValidate()
    {
        itemSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.name = abilityItem.abilityType + " pick up";
        itemSpriteRenderer.sprite = abilityItem.abilityIcon;
        itemSpriteRenderer.drawMode = SpriteDrawMode.Sliced;
        itemSpriteRenderer.size = new Vector2(2, 2);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
