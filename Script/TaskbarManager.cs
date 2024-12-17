using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

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

    public void DestroySelf()
    {
        Destroy(gameObject);
        Instance = null;
    }

    public void SpawnTaskbarWindow(Window window)
    {
        if (window != null)
        {
            WindowConfiguration config = window.GetConfiguration();  // Ensure this method exists and properly returns configuration
            GameObject iconInstance = Instantiate(config.taskbarPrefab, this.gameObject.transform); // Make sure taskbarParent is correctly set in the editor
            TaskbarWindow taskbarWindow = iconInstance.GetComponent<TaskbarWindow>();
            if (taskbarWindow != null)
            {
                taskbarWindow.Configure(window);
            } else
            {
            }
        }
    }
}
