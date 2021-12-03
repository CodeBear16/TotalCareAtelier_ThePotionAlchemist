using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SameResult
{
    public GameObject result;
    public List<Ingredient> ingredients;
}

public class Recipes : MonoBehaviour
{
    public List<SameResult> results;

    void Start()
    {
        for (int i = 0; i < Pot.instance.recipes.Count; i++)
        {
            results[i].result = Pot.instance.recipes[i].result;
            results[i].ingredients = Pot.instance.recipes[i].ingredients;
        }
    }

    void Update()
    {
        
    }
}
