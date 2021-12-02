using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;

    public virtual IEnumerator ReturnToSpawner()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(name + " ¿Áº“»Ø");
        IngredientPool<Ingredient>.instance.ResupplyObj(gameObject);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            StartCoroutine(ReturnToSpawner());
        if (collision.gameObject.CompareTag("MapBoundary"))
            StartCoroutine(ReturnToSpawner());
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("IngredientPool"))
            if (collision.gameObject.name == ingredientName + "Pool")
                IngredientPool<Ingredient>.instance.SpawnOneObj();
    }
}
