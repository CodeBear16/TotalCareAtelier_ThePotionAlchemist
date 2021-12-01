using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool<T> : MonoBehaviour where T : Ingredient
{
    public GameObject[] content;
    public Queue<GameObject> pool;
    const int maxSize = 10;

    [HideInInspector]
    public int randomNum;
    [HideInInspector]
    public GameObject tempObj;

    Transform spawnPos;

    void Start()
    {
        pool = new Queue<GameObject>();
        spawnPos = GetComponentInChildren<Transform>();

        FillingPool();
        ShowingBowl();

    }

    public void FillingPool()
    {
        while (pool.Count < maxSize)
        {
            randomNum = Random.Range(0, content.Length);
            tempObj = Instantiate(content[randomNum]);
            tempObj.transform.position = spawnPos.position;
            tempObj.transform.rotation = spawnPos.rotation;
            pool.Enqueue(tempObj);
        }
    }

    public void ShowingBowl()
    {

    }

}
