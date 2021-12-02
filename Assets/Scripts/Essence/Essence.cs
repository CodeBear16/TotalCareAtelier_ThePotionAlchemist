using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour, IMixFunc
{
    public static Queue<GameObject> essence;
    const int maxAmount = 2;

    public GameObject deburnPotion;
    public GameObject deparalysePotion;
    public GameObject detoxPotion;
    public GameObject explodePotion;

    [Tooltip("1: 물약 제조 성공, 2: 물약 제조 실패")]
    public AudioClip[] clips;

    void Start()
    {
        essence = new Queue<GameObject>();
        deburnPotion = Resources.Load<GameObject>("Potion/DeburnPotion");
        deparalysePotion = Resources.Load<GameObject>("Potion/DeparalysePotion");
        detoxPotion = Resources.Load<GameObject>("Potion/DetoxPotion");
        explodePotion = Resources.Load<GameObject>("Potion/ExplodePotion");
    }

    public void AddToEssence(GameObject component)
    {
        Debug.Log("재료를 추가한다.");
        essence.Enqueue(component);
        component.gameObject.SetActive(false);
    }

    public void CreatePotion()
    {
        Debug.Log("제작을 시작한다.");
        GameObject component0 = essence.Dequeue();
        GameObject component1 = essence.Dequeue();
        GameObject potion = CheckCombination(component0, component1);
        if (potion == null)
        {
            Debug.Log("제조 실패!");
            Destroy(potion);
            SoundController.instance.source.PlayOneShot(clips[1]);
        }
        else
        {
            Debug.Log(potion.name + " 제조 성공!");
            Instantiate(potion, transform.position + Vector3.up + Vector3.back, Quaternion.identity);
            SoundController.instance.source.PlayOneShot(clips[0]);
        }
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
                AddToEssence(other.gameObject);
                if (essence.Count == maxAmount)
                    CreatePotion();
            }
            else
                return;
        }
    }

    public GameObject CheckCombination(GameObject component0, GameObject component1)
    {
        if ((component0.GetComponent<Bone>() != null && component1.GetComponent<Egg>() != null)
              || (component0.GetComponent<Egg>() != null && component1.GetComponent<Bone>() != null))
        {
            return detoxPotion;
        }
        else if ((component0.GetComponent<Crystal>() != null && component1.GetComponent<Mushroom>() != null)
              || (component0.GetComponent<Mushroom>() != null && component1.GetComponent<Crystal>() != null))
        {
            return deparalysePotion;
        }
        else if ((component0.GetComponent<Seed>() != null && component1.GetComponent<Mushroom>() != null)
         || (component0.GetComponent<Mushroom>() != null && component1.GetComponent<Seed>() != null))
        {
            return detoxPotion;
        }
        else if ((component0.GetComponent<Flower>() != null && component1.GetComponent<Seed>() != null)
              || (component0.GetComponent<Seed>() != null && component1.GetComponent<Flower>() != null))
        {
            return explodePotion;
        }
        else
            return null;
    }
}
