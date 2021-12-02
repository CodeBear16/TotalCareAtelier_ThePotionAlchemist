using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Ingredient
{
    void Start()
    {
        ingredientName = "Mushroom";
    }

    public override IEnumerator ReturnToSpawner()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(name + " ¿Áº“»Ø");
        MushroomPool.instance.ResupplyObj(gameObject);
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("IngredientPool"))
            if (collision.gameObject.name == ingredientName + "Pool")
                MushroomPool.instance.SpawnOneObj();
    }
}
