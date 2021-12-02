using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PotionTest : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] resultPrefabs;
    enum POTION_TYPE
    {
        DEBURN, DEPARALYSE, DETOX, EXPLODE
    }
    List<string> potList = new List<string>();
    List<List<string>> recipeList = new List<List<string>>();
    Dictionary<List<string>, GameObject> resultDic = new Dictionary<List<string>, GameObject>();

    [Tooltip("물약 스폰 위치")]
    public Transform spawner;
    [Tooltip("1: 물약 제조 성공, 2: 물약 제조 실패")]
    public AudioClip[] clips;

    private void Start()
    {
        resultPrefabs = Resources.LoadAll<GameObject>("Potion/");
        CreateRecipe(resultPrefabs[(int)POTION_TYPE.DEBURN], "Flower", "Egg");
        CreateRecipe(resultPrefabs[(int)POTION_TYPE.DEPARALYSE], "Crystal", "Seed");
        CreateRecipe(resultPrefabs[(int)POTION_TYPE.DETOX], "Seed", "Mushroom");
        CreateRecipe(resultPrefabs[(int)POTION_TYPE.EXPLODE], "Bone", "Crystal");
    }

    public void CreateRecipe(GameObject resultObj, params string[] ingredients)
    {
        List<string> tempList = ingredients.ToList();
        recipeList.Add(tempList);
        resultDic.Add(tempList, resultObj);
    }

    public void CombineRecipe()
    {
        bool isPossible = true;
        foreach (List<string> currentRecipe in recipeList)
        {
            foreach (string ingredient in potList)
                isPossible &= currentRecipe.Contains(ingredient);
            if (isPossible)
                Instantiate(resultDic[currentRecipe], spawner);
        }
        potList.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ingredient>() != null)
        {
            if (other.GetComponent<OVRGrabbable>().isGrabbed != true)
            {
                potList.Add(other.GetComponent<Ingredient>().ingredientName);
                other.GetComponent<Ingredient>().ReturnToSpawner();
                if (potList.Count >= 2)
                    CombineRecipe();
            }
        }
    }
}