using UnityEngine;
using System.Collections;

public enum RotationAxes
{
    MouseX = 0,
    MouseY = 1
}
public class AlchemyMouseLook : MonoBehaviour
{
    public RotationAxes axes = RotationAxes.MouseX;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;
    float rotationX = 0F;

    void Update()
    {
            if (axes == RotationAxes.MouseX)
            {
                float _moveSpeed = Input.GetAxis("Mouse X");
                rotationX = (_moveSpeed / Screen.height) * sensitivityX * 5f;
            }
            else
            {
                float _moveSpeed = Input.GetAxis("Mouse Y");

                rotationY += (_moveSpeed / Screen.height) * sensitivityY * 5f;
            }

        if (axes == RotationAxes.MouseX)
            transform.Rotate(0, rotationX, 0);
        else
        {
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    public static Vector2 FixTouchDelta(Touch aT)
    {
        float dt = Time.deltaTime / aT.deltaTime;
        if (float.IsNaN(dt) || float.IsInfinity(dt))
            dt = 1.0f;

        return aT.deltaPosition * dt;
    }
}

