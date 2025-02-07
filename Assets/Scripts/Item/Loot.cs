using System;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemHP itemHP;

    void OnCollisionEnter2D(Collision2D collider)
    {
        GetComponent<Collider2D>().enabled = false;
        GlobalEventManager.TriggerOnMushroomEaten(itemHP.HPHeal);
        float posY = transform.position.y;

        LeanTween.alpha(gameObject, 0, 1f).setEase(LeanTweenType.easeInOutQuad);

        LeanTween
            .moveY(gameObject, posY + 2, 1f)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    private void OnValidate()
    {
        if (itemHP == null)
            return;
        GetComponent<SpriteRenderer>().sprite = itemHP.icon;
    }
}
