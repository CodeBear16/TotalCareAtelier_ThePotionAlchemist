using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    IGrabable target = null; //OnTrigger에 들어온 대상
    IGrabable grabObj = null; //스페이스를 눌러서 잡은 대상

    void Update()
    {
        if(target != null)  //OnTriggerEnter에 잡을 수 있는 대상이 있을 경우
        {
            if (Input.GetKeyDown(KeyCode.Space))    //스페이스바를 누르면, (VR의 경우 여기가 OVRInput.GetDown(OVRInput.PrimaryHandTrigger) 일 것임)
            {
                if (grabObj == null)    //잡고 있는 대상이 없을 때
                {
                    grabObj = target;       //잡고 있는 대상을 현재 OnTriggerEnter에 들어온 대상으로 만듬
                    target.Grab(transform); //IGrabable인터페이스의 Grab을 호출하여 잡는다
                }
                else                    //이미 잡고 있는 대상이 있을 때
                {
                    grabObj.Release();      //IGrabable인터페이스의 Grab을 호출하여 놓아주고
                    grabObj = null;         //잡고 있는 대상을 비워준다.
                }
            }
        }


        //이동 부//
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * 10f* Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -10f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)         //트리거 안에 들어왔을 때 호출 됨
    {
        Debug.Log("In : " + other.gameObject.name);
        if(other.GetComponent<IGrabable>() != null)     //충돌한 오브젝트에 IGrabable이 있으면,
        {
            Debug.Log("GrabTarget : " + other.gameObject.name);
            target = other.GetComponent<IGrabable>();   //IGrabable target에 넣어준다.
        }
    }

    private void OnTriggerExit(Collider other)          //트리거 밖으로 벗어났을 때 호출 됨
    {
        Debug.Log("Out : " + other.gameObject.name);
        if (other.GetComponent<IGrabable>() != null)    //충돌한 오브젝트에 IGrabable이 있으면,
        {
            target = null;                              //IGrabable target을 비워준다.
            Debug.Log("GrabTarget(release) : " + other.gameObject.name);
        }
    }
}
