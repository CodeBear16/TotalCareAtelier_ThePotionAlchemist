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
    public bool isSuccess = false; 
    public Animator animator;
    MonsterEffect monsterEffect;

    // Potion 받는 손 위치
    public Transform potionHand;
    public GameObject potion;

    GameTimer gameTimer;

    void OnEnable()
    {
        isSuccess = false;
        player = GameObject.Find("Player");
        exit = GameObject.Find("Exit");
        //potionHand = transform.Find("PotionPos");

        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monsterEffect = GetComponent<MonsterEffect>();
        gameTimer = transform.GetComponentInChildren<GameTimer>();
    }

    public void Setting(GameObject destination)
    {
        this.destination = destination;
        monsterState = "SpawnerToDestination";
        Walking();
    }

    public void Walking()
    {
        switch (monsterState)
        {
            // [---------------- spawner에서 destination으로 이동 ----------------]
            case "SpawnerToDestination":

                FindObjectOfType<DoorOpen>().Open();
                animator.SetBool("Walking", true);
                nav.SetDestination(destination.transform.position);
                Debug.Log(destination.name + "로 이동하는 " + gameObject.name);     
                break;

            // [---------------- destination에서 exit로 이동 ----------------]
            case "DestinationToExit":

                nav.SetDestination(exit.transform.position);
                Debug.Log(exit.name + "로 이동하는 " + gameObject.name);
                destination.GetComponent<MonsterDestination>().Leave();
                break;
        }
    }
    
    public void TakePotion(GameObject _potion)
    {
        Debug.Log("받음");

        if (_potion.GetComponent<Potion>().symptom == monsterEffect.effect.name)
            isSuccess = true;
        else
            isSuccess = false;

        gameTimer.time = 30;
        //potion.transform.position = potionHand.position;
        this.potion = _potion;
        potion.transform.parent = potionHand;
        potion.transform.localPosition = Vector3.zero;
        potion.transform.localRotation = potionHand.rotation;

        if (isSuccess)
        {
            GameManager.instance.Score += 10;
            GameManager.instance.HeroDistance += 20;
            animator.SetBool("Drinking", true);
            animator.SetBool("Walking", true);
            monsterEffect.HideEffect();
            // 효과음
        }
        else
        {
            GameManager.instance.Score -= 10;
            GameManager.instance.HeroDistance -= 10;
            GameManager.instance.MonsterUnhappy++;
            animator.SetBool("SadWalk", true);
            // 효과음
        }

        monsterState = "DestinationToExit";
        Walking();
    }

    void OnTriggerEnter(Collider other)
    {
        // monster가 destination에 도착하면 멈춤
        if (other.gameObject == destination)
        {
            transform.LookAt(player.transform.position);
            //nav.speed = 0;
            animator.SetBool("Walking", false);

            // 랜덤 animation
            int aniSelection = Random.Range(1, 6);
            animator.SetInteger("MonsterState", aniSelection);
            monsterEffect.ShowEffect();
            gameTimer.gameObject.SetActive(true);
            gameTimer.DecreaseTime();
            Debug.Log("시간 감소시작");
        }

        // monster가 exit에 도착하면 오브젝트 비활성화
        if (other.tag == "Exit")
        {
            Debug.Log("출구에 도착");
            //nav.speed = 0;
            Destroy(potion);
            MonsterSpawner.instance.ReturnToSpawner(gameObject);
        }
    }
}
