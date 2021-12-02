using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Ingredient
{
    void Start()
    {
        ingredientName = "Crystal";
    }

    public override IEnumerator ReturnToSpawner()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(name + " ¿Áº“»Ø");
        CrystalPool.instance.ResupplyObj(gameObject);
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("IngredientPool"))
            if (collision.gameObject.name == ingredientName + "Pool")
                CrystalPool.instance.SpawnOneObj();
    }
}
