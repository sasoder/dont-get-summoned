using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance { get; private set; }
    public float spawnPadding = 200f;
    public List<WindowConfiguration> windowConfigs;
    public GameObject failWindowPrefab;
    public GameObject pythonScriptWindowPrefab;
    public GameObject zoomWindowPrefab;
    public float timeBetweenWindowRemoval = 0.3f;
    public GameObject zoomWindowContainer;

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

    public Window SpawnWindow(int configIndex, CoworkerConfiguration coworker)
    {
        if (configIndex >= 0 && configIndex < windowConfigs.Count)
        {
            WindowConfiguration config = windowConfigs[configIndex];
            Vector2 spawnPosition = Vector2.zero;
            Transform spawnParent = this.gameObject.transform;
            if(configIndex == 2) {
                spawnPosition = getMiddlePosition();
                spawnParent = zoomWindowContainer.transform;
            } else {
                spawnPosition = getRandomPosition();
            }
            GameObject windowInstance = Instantiate(config.windowPrefab, spawnPosition, Quaternion.identity, spawnParent);  
            Window window = windowInstance.GetComponent<Window>();
            if (window != null)
            {
                window.Configure(config, coworker);
            }
            return window;
        }
        else
        {
            return null;
        }
    }

    public Vector2 getRandomPosition()
    {
        float x = Random.Range(spawnPadding, Screen.width - spawnPadding);
        float y = Random.Range(spawnPadding, Screen.height - spawnPadding);
        return new Vector2(x, y);
    }

    public Vector2 getMiddlePosition()
    {
        float x = Screen.width / 2;
        float y = Screen.height / 2;
        return new Vector2(x, y);
    }

    // Method to start the coroutine
    public void CloseAllWindows() {
        StartCoroutine(CloseAllWindowsWithDelay());
    }
    // Coroutine to close all windows with a delay
    private IEnumerator CloseAllWindowsWithDelay() {
        foreach (Window window in GetComponentsInChildren<Window>()) {
            if (window.gameObject.tag == "CMD") {
                continue; // Skip if the window has the "CMD" tag
            }

            window.CompleteTaskScript(); // Close the window gameobject

            // Wait for the specified time before destroying the next window
            yield return new WaitForSeconds(this.timeBetweenWindowRemoval);
        }
    }
}
