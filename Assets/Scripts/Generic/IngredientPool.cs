using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool<T> : Singleton<IngredientPool<T>> where T : Ingredient
{
    [HideInInspector]
    public GameObject[] contentPrefabs;
    public string contentName;

    public Queue<GameObject> contentPoolQueue = new Queue<GameObject>();
    const int maxSize = 20;

    [HideInInspector]
    public int randomNum;
    [HideInInspector]
    public GameObject tempObj;

    Vector3 spawnPos;

    void Start()
    {
        contentName = name.Replace("Pool", string.Empty);
        contentPrefabs = Resources.LoadAll<GameObject>("Ingredient/" + contentName);

        spawnPos = transform.position + Vector3.up * 1;

        StartCoroutine(FillAllPool());
        SpawnOneObj();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SpawnAllObj();
    }

    IEnumerator FillAllPool()
    {
        while (contentPoolQueue.Count < maxSize)
        {
            randomNum = Random.Range(0, contentPrefabs.Length);
            tempObj = Instantiate(contentPrefabs[randomNum]);
            ResupplyObj(tempObj);
            yield return null;
        }
    }

    public void SpawnOneObj()
    {
        tempObj = contentPoolQueue.Dequeue();
        tempObj.transform.parent = null;
        tempObj.SetActive(true);
    }

    public void SpawnAllObj()
    {
        while (contentPoolQueue.Count != 0)
            SpawnOneObj();
    }

    public virtual void ResupplyObj(GameObject tempObj)
    {
        tempObj.transform.position = spawnPos;
        tempObj.transform.parent = transform;
        tempObj.SetActive(false);
        contentPoolQueue.Enqueue(tempObj);
    }
}
