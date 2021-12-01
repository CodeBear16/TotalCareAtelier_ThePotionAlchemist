using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool<T> : Singleton<IngredientPool<T>> where T : Ingredient
{
    public GameObject[] content;
    public Queue<GameObject> pool;
    public Queue<GameObject> usedPool;
    const int maxSize = 20;

    [HideInInspector]
    public int randomNum;
    [HideInInspector]
    public GameObject tempObj;

    Transform spawnPos;

    void Start()
    {
        pool = new Queue<GameObject>();
        usedPool = new Queue<GameObject>();
        spawnPos = GetComponentInChildren<Transform>();

        FillingPool();
        ShowContent();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            FireContent();
    }

    public void FillingPool()
    {
        while (pool.Count < maxSize)
        {
            randomNum = Random.Range(0, content.Length);
            tempObj = Instantiate(content[randomNum]);
            tempObj.transform.position = spawnPos.position;
            tempObj.transform.rotation = spawnPos.rotation;
            tempObj.SetActive(false);
            pool.Enqueue(tempObj);
        }
    }

    public void ShowContent()
    {
        tempObj = pool.Dequeue();
        tempObj.SetActive(true);
        usedPool.Enqueue(tempObj);
    }

    public void FireContent()
    {
        while (pool.Count != 0)
        {
            tempObj = pool.Dequeue();
            tempObj.SetActive(true);
        }
    }

}
