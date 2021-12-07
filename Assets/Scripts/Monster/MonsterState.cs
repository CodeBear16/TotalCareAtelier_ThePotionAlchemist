using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    #region Monster's Lifecycle 몬스터의 루틴
    ///public MonsterSpawner monsterSpawner;
    ///public List<GameObject> destinationList;
    public GameObject destination;
    public GameObject exit;

    NavMeshAgent nav;
    GameObject player;
    #endregion

    // switch 값
    public string monsterState;

    ///Transform playerPosition;
    public bool isSuccess = false; // ------------------------------ player와 연동해야 함, 삭제 필

    Animator animator;
    MonsterEffect monsterEffect;

    // Potion 받는 손 위치
    public Transform potionHand;
    public GameObject potion;

    GameTimer gameTimer;

    ///string state;

    void OnEnable()
    {
        isSuccess = false;
        player = GameObject.Find("Player");
        exit = GameObject.Find("Exit");
        potionHand = transform.Find("PotionPos");

        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monsterEffect = GetComponent<MonsterEffect>();
        gameTimer = transform.Find("Canvas").GetComponentInChildren<GameTimer>();
    }

    public void Setting(GameObject destination)
    {
        this.destination = destination;
        ///destinationList = MonsterSpawner.destinationList;
        monsterState = "SpawnerToDestination";
        Walking();
    }

    public void Walking()
    {
        switch (monsterState)
        {
            // [---------------- spawner에서 destination으로 이동 ----------------]
            case "SpawnerToDestination":

                ///if (destinationList.Count <= 0) break;

                GameObject.FindGameObjectWithTag("Door").GetComponent<DoorOpen>().Open();
                animator.SetBool("Walking", true);
                ///playerPosition = player.transform;
                ///int selectDestination = Random.Range(0, destinationList.Count);
                ///GameObject destination = destinationList[selectDestination];

                ///if (destination.activeSelf == false)
                ///{
                nav.SetDestination(destination.transform.position);
                ///destination.SetActive(true);
                ///this.destination = destination;
                ///MonsterSpawner.destinationList.RemoveAt(selectDestination);
                Debug.Log(destination.name + "로 이동하는 " + gameObject.name);
                ///}
                break;

            // [---------------- destination에서 exit로 이동 ----------------]
            case "DestinationToExit":

                nav.SetDestination(exit.transform.position);
                Debug.Log(exit.name + "로 이동하는 " + gameObject.name);
                ///MonsterSpawner.destinationList.Add(this.destination);
                ///this.destination.SetActive(false);
                destination.GetComponent<MonsterDestination>().Leave();
                break;
        }
    }
    
    public void TakePotion(GameObject potion)
    {
        potion.transform.position = potionHand.position;
        potion.transform.parent = potionHand;
        this.potion = potion;

        if (isSuccess)
        {
            GameManager.instance.Score += 10;
            HeroComing.instance.comingDistance -= Time.deltaTime * 2;
            animator.SetBool("Drinking", true);
            animator.SetBool("Walking", true);
            monsterEffect.HideEffect();
        }

        else
        {
            GameManager.instance.Score -= 10;
            HeroComing.instance.comingDistance += Time.deltaTime * 2;
            animator.SetBool("SadWalk", true);
            GameObject.Find("MonsterUnHappy").transform.GetChild(GameManager.instance.monsterUnhappy).gameObject.SetActive(true);
            GameManager.instance.MonsterUnhappy++;
        }

        monsterState = "DestinationToExit";
        Walking();
    }

    void OnTriggerEnter(Collider other)
    {
        // monster가 destination에 도착하면 멈춤
        ///if (other.tag == "Destination")
        if (other.gameObject == destination)
        {
            ///if (other.gameObject == destination)
            ///{
            transform.LookAt(player.transform.position);
            nav.speed = 0;
            animator.SetBool("Walking", false);
            destination.GetComponent<MonsterDestination>().Occupy();

            // 랜덤 animation
            int aniSelection = Random.Range(1, 6);
            animator.SetInteger("MonsterState", aniSelection);

            monsterEffect.ShowEffect();
            gameTimer.DecreaseTime();
            Debug.Log("시간 감소시작");
            ///}
        }

        // monster가 exit에 도착하면 오브젝트 비활성화
        if (other.tag == "Exit")
        {
            Debug.Log("출구에 도착");
            nav.speed = 0;
            Destroy(potion);
            MonsterSpawner.instance.ReturnToSpawner(gameObject);
            ///gameObject.SetActive(false);
        }

        // potion을 받았을 때
        if (other.tag == "Potion")
        {
            if (other.GetComponent<OVRGrabbable>().isGrabbed == false)
            {
                if (other.GetComponent<Potion>().symptom == monsterEffect.effect.name)
                    isSuccess = true;
                else
                    isSuccess = false;
                //if (포션 이름 == particles[effectSelection].name) isSuccess = true;
                //else isSuccess = false;
                gameTimer.ResetTime();
                TakePotion(other.gameObject);
            }
        }
    }
}
