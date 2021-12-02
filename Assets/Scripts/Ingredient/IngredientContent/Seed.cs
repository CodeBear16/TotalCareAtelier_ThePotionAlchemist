using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Ingredient
{
    void Start()
    {
        ingredientName = "Seed";
    }

    public override IEnumerator ReturnToSpawner()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(name + " ¿Áº“»Ø");
        SeedPool.instance.ResupplyObj(gameObject);
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("IngredientPool"))
            if (collision.gameObject.name == ingredientName + "Pool")
                SeedPool.instance.SpawnOneObj();
    }
}
