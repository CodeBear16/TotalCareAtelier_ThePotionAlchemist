using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Ingredient
{
    void Start()
    {
        ingredientName = "Flower";
    }

    public override IEnumerator ReturnToSpawner()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(name + " ¿Áº“»Ø");
        FlowerPool.instance.ResupplyObj(gameObject);
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("IngredientPool"))
            if (collision.gameObject.name == ingredientName + "Pool")
                FlowerPool.instance.SpawnOneObj();
    }
}
