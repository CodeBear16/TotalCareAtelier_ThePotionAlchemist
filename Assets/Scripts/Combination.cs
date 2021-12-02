using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combination
{
    public static int deburnPotion = 0;
    public static int deparalysePotion = 1;
    public static int detoxPotion = 2;
    public static int explodePotion = 3;
    public static int CheckCombi(this int value, GameObject component0, GameObject component1)
    {
        if ((component0.GetComponent<Flower>() != null && component1.GetComponent<Egg>() != null)
              || (component0.GetComponent<Egg>() != null && component1.GetComponent<Flower>() != null))
            return deburnPotion;
        else if ((component0.GetComponent<Crystal>() != null && component1.GetComponent<Seed>() != null)
              || (component0.GetComponent<Seed>() != null && component1.GetComponent<Crystal>() != null))
            return deparalysePotion;
        else if ((component0.GetComponent<Seed>() != null && component1.GetComponent<Mushroom>() != null)
         || (component0.GetComponent<Mushroom>() != null && component1.GetComponent<Seed>() != null))
            return detoxPotion;
        else if ((component0.GetComponent<Bone>() != null && component1.GetComponent<Crystal>() != null)
              || (component0.GetComponent<Crystal>() != null && component1.GetComponent<Bone>() != null))
            return explodePotion;
        else
            return -1;
    }
}
