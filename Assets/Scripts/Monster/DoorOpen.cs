using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator m_Animator;
    public string m_AniamtionName = "Play";
    public bool m_ShoulPlaySound = true;
    public bool m_ShouldReverse = false;
    public AudioClip[] m_Clips;
    private AudioSource m_AudioSource;
    private int m_Reverse = 0;

    public MonsterSpawner _monster;
    public GameObject[] _monsters;

    private void Start()
    {
        _monsters = new GameObject[6];
        _monsters = _monster.monsters;

        if (m_ShoulPlaySound)
            m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RaycastHit hit;
        int rayCastLenght = 1;

        if(Physics.Raycast(transform.position, transform.forward, out hit, rayCastLenght))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.blue, 2);
            if (hit.collider.gameObject.tag == "Monster")
            {
                if (m_ShouldReverse)
                {
                    m_Animator.Play(m_AniamtionName + m_Reverse.ToString());

                    if (m_ShoulPlaySound && m_AudioSource != null && !m_AudioSource.isPlaying)
                        m_AudioSource.PlayOneShot(m_Clips[m_Reverse]);

                    m_Reverse++;

                    m_Reverse = m_Reverse >= 2 ? 0 : m_Reverse;
                }
                else
                {
                    m_Animator.Play(m_AniamtionName);

                    if (m_ShoulPlaySound && m_AudioSource != null && !m_AudioSource.isPlaying)
                        m_AudioSource.PlayOneShot(m_Clips[m_Reverse]);
                }
            }
        }
    }
}
