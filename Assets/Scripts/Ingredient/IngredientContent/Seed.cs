using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Ingredient
{
    public override Ingredient Add()
    {
        gameObject.SetActive(false);
        return this;
    }
}
