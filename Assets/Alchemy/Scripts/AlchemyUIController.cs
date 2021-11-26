using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyUIController : MonoBehaviour
{
    public static AlchemyUIController m_Instance;

    public GameObject m_ActionText;

    void Awake()
    {
        m_Instance = this;
    }

    internal void ShowActionText(bool _Show)
    {
       m_ActionText.SetActive(_Show);
    }
}
