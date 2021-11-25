using UnityEngine;
using System.Collections;

public class PlayersController : MonoBehaviour
{
    private int m_LayerMask;

    private CharacterController m_CharacterController;
    private Vector2 m_Input;
    private Vector3 m_MoveDirection = Vector3.zero;
    private float m_Gravity = 50.0f;
    public float m_WalkSpeed = 5f;
    private Camera m_CameraMain;

    // Use this for initialization
    void Awake()
    {
        m_LayerMask = LayerMask.GetMask("Interaction");
        m_CharacterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_CameraMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float speed;

        GetInput(out speed);

        if (m_CharacterController.isGrounded)
        {
            m_MoveDirection = new Vector3(m_Input.x, 0, m_Input.y);
            m_MoveDirection = transform.TransformDirection(m_MoveDirection);
            m_MoveDirection *= speed;
        }

        m_MoveDirection.y -= m_Gravity * Time.deltaTime;
        m_CharacterController.Move(m_MoveDirection * Time.deltaTime);

        RaycastHit hit;

        if (Physics.Raycast(m_CameraMain.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)), out hit, 2f, m_LayerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ActionController _AC = hit.collider.GetComponent<ActionController>();

                if (_AC != null)
                    _AC.DoAction();
            }

            UIController.m_Instance.ShowActionText(true);
        }
        else
            UIController.m_Instance.ShowActionText(false);
    }


    private void GetInput(out float speed)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        speed = m_WalkSpeed;

        m_Input = new Vector2(horizontal, vertical);

        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    }
}