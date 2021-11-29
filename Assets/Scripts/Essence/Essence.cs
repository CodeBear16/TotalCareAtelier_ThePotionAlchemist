using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour, IMixFunc
{
    public static Queue<Ingredient> essence;
    const int maxAmount = 2;
    bool creatable = false;

    void Start()
    {
        essence = new Queue<Ingredient>();
    }

    void Update()
    {
        if (creatable)
            if (Input.GetKeyDown(KeyCode.Space))
                CreatePotion();
        if (Input.GetKeyDown(KeyCode.Return))
            ClearEssence();
    }

    public void AddToEssence(Ingredient component)
    {
        Debug.Log("재료를 추가한다.");
        essence.Enqueue(component);
        component.gameObject.SetActive(false);
    }

    public void CreatePotion()
    {
        Debug.Log("제작을 시작한다.");
        Potion potion = new Potion();
        Ingredient component0 = essence.Dequeue();
        Ingredient component1 = essence.Dequeue();
        potion.CheckCombi(component0, component1);
        if (potion == null)
        {
            Debug.Log("제조 실패!");
            Destroy(potion);
        }
        else
        {
            Debug.Log(potion.name + " 제조 성공!");
            Instantiate(potion);
        }
        creatable = false;
    }

    public void ClearEssence()
    {
        while (essence.Count != 0)
            essence.Dequeue();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("냄비에 닿았다.");
        if (other.GetComponent<IAddFunc>() != null)
        {
            Debug.Log("재료를 인식했다.");
            if (essence.Count < maxAmount)
            {
                AddToEssence(other.GetComponent<IAddFunc>().Add());
                if (essence.Count == maxAmount)
                    creatable = true;
            }
            else
                return;
        }
    }
}
