using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IAddFunc
{
    public virtual Ingredient Add()
    {
        gameObject.SetActive(false);
        return this;
    }
}
