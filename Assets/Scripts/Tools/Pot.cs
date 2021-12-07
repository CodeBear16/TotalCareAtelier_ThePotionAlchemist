using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Recipe
{
    [Tooltip("Result: 포션, Ingredients: 재료")]
    public GameObject result;
    public List<Ingredient> ingredients;
}

public class Pot : Singleton<Pot>
{
    #region Audio Clips
    public AudioClip[] ingredientPutClips;
    [Tooltip("0: DEBURN\n1: DEPARALYSE\n2: DETOX\n3: EXPLODE")]
    public AudioClip[] resultGoodClips;
    Dictionary<GameObject, AudioClip> clipDic = new Dictionary<GameObject, AudioClip>();
    public AudioClip[] resultFailClips;
    public AudioClip loopBoilingClip;
    AudioSource source;
    #endregion

    public GameObject particle;

    #region Recipes
    public Transform createSpot;
    public List<Recipe> recipes;

    List<string> potList = new List<string>();
    List<List<string>> recipeList = new List<List<string>>();
    Dictionary<List<string>, GameObject> resultDic = new Dictionary<List<string>, GameObject>();
    #endregion

    void Start()
    {
        for (int i = 0; i < recipes.Count; i++)
            CreateRecipe(recipes[i].result, recipes[i].ingredients, i);
        source = GetComponent<AudioSource>();
        source.clip = loopBoilingClip;
        source.loop = true;
        source.Play();
    }

    public void CreateRecipe(GameObject resultObj, List<Ingredient> ingredients, int i)
    {
        List<string> tempList = new List<string>();
        for (int j = 0; j < ingredients.Count; j++)
            tempList.Add(ingredients[j].ingredientName);
        recipeList.Add(tempList);
        resultDic.Add(tempList, resultObj);
        clipDic.Add(resultObj, resultGoodClips[i]);
    }

    public void CombineRecipe()
    {
        bool isPossible = true;

        int count = 0;

        foreach (List<string> currentRecipe in recipeList)
        {
            Debug.Log(count + "번 레시피의 재료 수: " + currentRecipe.Count);
            Debug.Log("투입된 재료 수: " + potList.Count);

            if (currentRecipe.Count != potList.Count)
                continue;

            foreach (string ingredient in currentRecipe)
                Debug.Log(count + "번 레시피의 재료: " + ingredient);

            foreach (string ingredient in potList)
            {
                Debug.Log("투입된 재료: " + ingredient);
                isPossible &= currentRecipe.Contains(ingredient);
                Debug.Log(isPossible);
            }

            if (isPossible)
            {
                Instantiate(resultDic[currentRecipe], createSpot);
                Debug.Log(resultDic[currentRecipe].name + "포션 조제 성공!");
                source.PlayOneShot(clipDic[resultDic[currentRecipe]]);
                potList.Clear();
                return;
            }

            isPossible = true;
            count++;
        }
        StartCoroutine(PotionFail());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ingredient>() != null || other.GetComponent<OVRGrabbable>().isGrabbed == false)
        {
            Ingredient content = other.GetComponent<Ingredient>();
            if (potList.Contains(content.ingredientName) == false)
                PutIngredient(content);
        }

        if (other.GetComponent<IMixFunc>() != null)
            CombineRecipe();
    }

    private void PutIngredient(Ingredient content)
    {
        Debug.Log(content.ingredientName + "를 투하했다.");
        source.PlayOneShot(ingredientPutClips[Random.Range(0, ingredientPutClips.Length)]);
        potList.Add(content.ingredientName);
        content.gameObject.SetActive(false);
        StartCoroutine(content.ReturnToSpawner());
    }

    private IEnumerator PotionFail()
    {
        Debug.Log("조제 실패... 냄비를 비웠다.");
        source.PlayOneShot(resultFailClips[Random.Range(0, resultFailClips.Length)]);
        particle.SetActive(true);
        potList.Clear();
        yield return new WaitForSeconds(3);
        particle.SetActive(false);
    }
}
