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

    private void Start()
    {
        if (m_ShoulPlaySound)
            m_AudioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        Debug.Log("¹® ¿ÀÇÂ");

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
