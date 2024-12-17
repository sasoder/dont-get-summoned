using UnityEngine;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        Instance = null;
    }

    public void CreateNotification(int relatedWindowConfigIndex)
    {
        GameObject notificationPrefab = WindowManager.Instance.windowConfigs[relatedWindowConfigIndex].notificationPrefab;
        GameObject notificationGO = Instantiate(notificationPrefab, this.gameObject.transform);
        NotificationWindow notification = notificationGO.GetComponent<NotificationWindow>();
        if (notification != null)
        {
            notification.Initialize(relatedWindowConfigIndex);
        }
    }


}
