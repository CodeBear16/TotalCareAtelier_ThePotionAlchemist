using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : Ingredient
{
    void Start()
    {
        ingredientName = "Bone";
    }

    public override IEnumerator ReturnToSpawner()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(name + " ¿Áº“»Ø");
        BonePool.instance.ResupplyObj(gameObject);
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("IngredientPool"))
            if (collision.gameObject.name == ingredientName + "Pool")
                BonePool.instance.SpawnOneObj();
    }
}
