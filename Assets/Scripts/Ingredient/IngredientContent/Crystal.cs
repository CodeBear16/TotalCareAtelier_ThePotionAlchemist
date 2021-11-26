using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Ingredient
{
    public override Ingredient Add()
    {
        gameObject.SetActive(false);
        return this;
    }
}
