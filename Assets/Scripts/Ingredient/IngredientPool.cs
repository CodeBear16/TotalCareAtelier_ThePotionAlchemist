using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool<T> : Singleton<IngredientPool<T>> where T : Ingredient
{
    private GameObject[] contentPrefabs;
    private string contentName;

    public Queue<GameObject> contentPoolQueue = new Queue<GameObject>();
    const int maxSize = 20;

    private int randomNum;
    private GameObject tempObj;

    public Transform parent;
    private Vector3 spawnPos;

    private void Start()
    {
        contentName = name.Replace("Pool", string.Empty);
        contentPrefabs = Resources.LoadAll<GameObject>("Ingredient/" + contentName);

        spawnPos = transform.position + Vector3.up * 1;

        StartCoroutine(FillAllPool());
        SpawnOneObj();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SpawnAllObj();
    }

    private IEnumerator FillAllPool()
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
        if (contentPoolQueue.Count != 0)
        {
            tempObj = contentPoolQueue.Dequeue();
            tempObj.SetActive(true);
        }
    }

    public void SpawnAllObj()
    {
        while (contentPoolQueue.Count != 0)
            SpawnOneObj();
    }

    public virtual void ResupplyObj(GameObject tempObj)
    {
        tempObj.transform.position = spawnPos;
        tempObj.transform.parent = parent;
        tempObj.SetActive(false);
        contentPoolQueue.Enqueue(tempObj);
    }
}
