using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;
    RaycastHit hit;
    private bool isPouring = false;
    private WaterStream currentStream = null;

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;
        //Debug.Log(transform.forward.y);
        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
                StartPour();
            else
                EndPour();
        }
    }

    private void StartPour()
    {
        Debug.Log("스타트");
        currentStream = CreateStream();
        currentStream.Begin();
        
    }

    private void EndPour()
    {
        Debug.Log("종료");
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle()
    {
        //Debug.Log (transform.up.y);
        //return Mathf.Atan2(transform.position.y, transform.forward.z) * Mathf.Rad2Deg;
        return transform.up.y * Mathf.Rad2Deg;

    }

    private WaterStream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        streamObject.transform.parent = origin;
        return streamObject.GetComponent<WaterStream>();
    }
}