using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Ingredient
{
    void Start()
    {
        ingredientName = "Egg";
    }

    public override IEnumerator ReturnToSpawner()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(name + " ¿Áº“»Ø");
        EggPool.instance.ResupplyObj(gameObject);
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("IngredientPool"))
            if (collision.gameObject.name == ingredientName + "Pool")
                EggPool.instance.SpawnOneObj();
    }
}
