using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool<T> : MonoBehaviour where T : Ingredient
{
    [HideInInspector]
    public GameObject[] content;
    string contentName;
    public Queue<GameObject> pool;
    public Queue<GameObject> usedPool;
    const int maxSize = 20;

    [HideInInspector]
    public int randomNum;
    [HideInInspector]
    public GameObject tempObj;

    Vector3 spawnPos;

    void Start()
    {
        contentName = name.Replace("Pool", string.Empty);
        content = Resources.LoadAll<GameObject>("Ingredient/" + contentName);
        pool = new Queue<GameObject>();
        usedPool = new Queue<GameObject>();

        spawnPos = transform.position + Vector3.up * 1; 

        FillingPool();
        CallOnePool();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            FireAllPool();
    }

    public void FillingPool()
    {
        while (pool.Count < maxSize)
        {
            randomNum = Random.Range(0, content.Length);
            tempObj = Instantiate(content[randomNum]);
            tempObj.transform.position = spawnPos;
            tempObj.SetActive(false);
            tempObj.transform.parent = transform;
            pool.Enqueue(tempObj);
        }
    }
    public void CallOnePool()
    {
        tempObj = pool.Dequeue();
        tempObj.SetActive(true);
        usedPool.Enqueue(tempObj);
    }

    public void FireAllPool()
    {
        while (pool.Count != 0)
        {
            tempObj = pool.Dequeue();
            tempObj.SetActive(true);
        }
    }
}
