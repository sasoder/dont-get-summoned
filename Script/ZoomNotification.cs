using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ZoomNotification : MonoBehaviour
{

    private WindowConfiguration windowConfig; // Reference to the window configuration
    public RectTransform timer;
    private float remainingTime;
    private float totalTime;


    void Awake()
    {
        totalTime = 10f;
        remainingTime = totalTime;
    }

    private CoworkerConfiguration getCoworker() {
        return windowConfig.coworkerConfiguration[Random.Range(0, windowConfig.coworkerConfiguration.Count)];
    }

    void Update()
    {

        // Check if time has run out
        if (remainingTime <= 0) {
            GameManager.Instance.onTaskFail(false);
            Destroy(gameObject);
        }
    }

}
