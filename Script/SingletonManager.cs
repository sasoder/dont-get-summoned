using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{

    public static SingletonManager Instance { get; private set; }
    private void Awake()
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

    public void DestroyAllSingletons()
    {
        SoundManager.Instance?.DestroySelf();
        GameManager.Instance?.DestroySelf();
        // TaskManager.Instance?.DestroySelf();
        // NotificationManager.Instance?.DestroySelf();
        // WindowManager.Instance?.DestroySelf();

        // Add other singleton destruction calls here
    }
}

