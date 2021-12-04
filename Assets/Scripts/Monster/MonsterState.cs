using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    GameObject exit;
    
    GameObject player;
    Transform playerPosition;
    public bool isSuccess = false; // ------------------------------ player와 연동해야 함, 삭제 필

    // monster 이동
    public NavMeshAgent nav;
    public List<GameObject> destinations;
    public GameObject returnDestination = null;

    public Animator animator;
    MonsterEffect monsterEffect = new MonsterEffect();

    // monster state
    string monsterState = "MonsterState";

    // Potion 받는 위치
    public Transform potionHand;

    // switch 값
    public string state;

    private void OnEnable()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        potionHand = transform.Find("PotionPos");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isSuccess = false;
    }

    public void Setting()
    {
        destinations = MonsterSpawner.destinationsSpotList;
    }

    public void Walking()
    {
        switch (state)
        {
            // [---------------- spawner에서 destination으로 이동 ----------------]
            case "SpawnerToDestination":

                if (destinations.Count <= 0) break;

                GameObject.FindGameObjectWithTag("Door").GetComponent<DoorOpen>().Open();
                animator.SetBool("Walking", true);
                playerPosition = player.transform;
                int selectDestination = Random.Range(0, destinations.Count);
                GameObject destination = destinations[selectDestination];

                if (destination.activeSelf == false)
                {
                    nav.SetDestination(destination.transform.position);
                    destination.SetActive(true);
                    returnDestination = destination;
                    MonsterSpawner.destinationsSpotList.RemoveAt(selectDestination);
                    Debug.Log(gameObject.name + "이동 성공" + destination.name);
                }

                break;

            // [---------------- destination에서 exit로 이동 ----------------]
            case "DestinationToExit":

                exit = GameObject.Find("Exit");
                nav.SetDestination(exit.transform.position);
                MonsterSpawner.destinationsSpotList.Add(returnDestination);
                returnDestination.SetActive(false);

                break;
        }
    }
    
    public void TakePotion()
    {
        if (isSuccess)
        {
            GameManager.instance.Score += 10;
            animator.SetBool("Drinking", true);
            animator.SetBool("Walking", true);
            monsterEffect.ChangeEffect();
        }

        else
        {
            GameManager.instance.Score -= 10;
            animator.SetBool("SadWalk", true);
        }

        state = "DestinationToExit";
        Walking();
    }

    public void OnTriggerEnter(Collider other)
    {
        // monster가 destination에 도착하면 멈춤
        if (other.tag == "Destination")
        {
            if (other.gameObject == returnDestination)
            {
                transform.LookAt(playerPosition.position);
                nav.speed = 0;

                // 랜덤 animation
                int aniSelection = Random.Range(1, 6);
                animator.SetInteger(monsterState, aniSelection);
            }
        }

        // monster가 exit에 도착하면 오브젝트 비활성화
        if (other.tag == "Exit")
        {
            Debug.Log("exit 태그에 들어옴");
            nav.speed = 0;
            gameObject.SetActive(false);
        }

        // potion을 받았을 때
        if (other.tag == "Potion")
        {
            // -------------------------------- OVRGrabbable 오류 떠요 namespace 없대요ㅜㅜ
            //if (other.GetComponent<OVRGrabbable>().isGrabbed == false)
            //{
            //    other.transform.position = potionHand.transform.position;

            //    //if (포션 이름 == particles[effectSelection].name) isSuccess = true;
            //    //else isSuccess = false;

            //    TakePotion();
            //}
        }
    }
}
