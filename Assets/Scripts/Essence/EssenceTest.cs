using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceTest : MonoBehaviour, IMixFunc
{
    public static Queue<Ingredient> mixture;
    const int mixtureAmount = 2;

    void Start()
    {
        mixture = new Queue<Ingredient>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MixEssence();
    }

    public void AddMixture(Ingredient component)
    {
        Debug.Log("재료를 추가한다.");
        if (mixture.Count < mixtureAmount)
        {
            mixture.Enqueue(component);
            component.gameObject.SetActive(false);
        }
    }

    public void MixEssence()
    {
        Debug.Log("에센스를 섞는다.");
        if (mixture.Count == 2)
            CheckCombi();
        else
            Debug.Log("재료가 덜 들어갔다.");
    }

    public void CheckCombi()
    {
        Debug.Log("조합을 확인한다.");
        Potion potion = new Potion();
        Ingredient component0 = mixture.Dequeue();
        Ingredient component1 = mixture.Dequeue();
        potion.CheckCombi(component0, component1);
        if (potion == null)
        {
            Debug.Log("실패!");
            Destroy(potion);
        }
        else
        {
            Debug.Log(potion.name + "제조 성공!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IAddFunc>() != null)
        {
            AddMixture(other.GetComponent<Ingredient>());
        }
    }
}
