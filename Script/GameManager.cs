using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // 12
    private float timeBetweenWindows = 12f; // Time interval to spawn windows
    private float timeUntilNextWindow;
    private float happyMeter = 100f;
    public float happyMeterMin = 0f;
    public TextMeshProUGUI timeText;
    public float elapsedTime = 0f;
    public float multiplier = 1f;
    private float decayConstant = 0.00578f;
    public GameObject satisFactionMeter;
    private SatisFactionMeter satisFactionMeterComponent;
    public bool gameOver;
    public GameObject pythonProgram;
    public GameObject background;
    private bool isVirus;
    private float spawnRate = 1.0f; // Seconds between spawns
    private float nextSpawnTime = 0f;
    public GameObject enemy;
    public RectTransform spawnArea;
    private float timeBetweenZoomWindows = 40f;
    private float timeUntilNextZoomWindow;
    public string highScoreString;
    public int amtCompletedTasks;
    public int amtFailedTasks;
    public AudioClip bossNotificationSound;
    private float timeUntilFirstTask;
    private bool hasGivenTask;


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

    void Start()
    {
        Application.targetFrameRate = 60;
        hasGivenTask = false;
        timeUntilFirstTask = 10f;
        amtCompletedTasks = 0;
        amtFailedTasks = 0;
        highScoreString = "00:00";
        timeUntilNextZoomWindow = timeBetweenZoomWindows;
        // 100
        happyMeter = 100f;
        gameOver = false;
        satisFactionMeterComponent = satisFactionMeter.GetComponent<SatisFactionMeter>();
        satisFactionMeterComponent.UpdateSatisFaction(happyMeter);
        timeUntilNextWindow = 3f;
    }

    public void onTaskSuccess() 
    {
        amtCompletedTasks++;
        UpdateSatisFaction(5f);
    }

    public void onTaskSuccessMap(float score) {
        if(score >= 0) {
            amtCompletedTasks++;
        }  else {
            amtFailedTasks++;
        }
        UpdateSatisFaction(score);
    }

    public void onTaskFail(bool spawnTaskFail) {
        if (spawnTaskFail) {
            SpawnFailWindow();
        }
        amtFailedTasks++;
        UpdateSatisFaction(-10f);

    }

    public void onTaskFailPsycho(bool instaDeath) {
        amtFailedTasks++;
        if (instaDeath) {
            SoundManager.Instance.StopBackgroundMusic();
            SceneManager.LoadScene("PsychoEnding");
            isGameOverHandled = true;
        } else {
            UpdateSatisFaction(-30f);
        }
    }

    public void onTaskFailVirus() {
        isVirus = true;
        amtFailedTasks++;
        UpdateSatisFaction(-10f);
    }

    public void UpdateSatisFaction(float satisFaction) {
        happyMeter += satisFaction;

        if(happyMeter <= happyMeterMin) {
            gameOver = true;
            happyMeter = happyMeterMin;
        }

        if(happyMeter >= 100f) {
            happyMeter = 100f;
        }
            
        satisFactionMeterComponent.UpdateSatisFaction(happyMeter);
    }

    public void SpawnFailWindow() {
        Window relatedWindow = WindowManager.Instance.SpawnWindow(0, null);
        TaskManager.Instance.SpawnTaskbarWindow(relatedWindow);
    }
    public void SpawnPythonScriptWindow() {
        Window relatedWindow = WindowManager.Instance.SpawnWindow(1, null);
        TaskManager.Instance.SpawnTaskbarWindow(relatedWindow);
    }

    public void SpawnPythonProgram() {
        Instantiate(pythonProgram, WindowManager.Instance.getRandomPosition(), Quaternion.identity, background.transform);
    }

    public void SpawnZoomWindow() {
        // Instantiate
        Window relatedWindow = WindowManager.Instance.SpawnWindow(2, null);
        TaskManager.Instance.SpawnTaskbarWindow(relatedWindow);
    }


    public void CompleteAllTasks() {
        WindowManager.Instance.CloseAllWindows();
    }

    private bool isGameOverHandled = false; 

    void Update()
    {

        if(gameOver && !isGameOverHandled) {
            SoundManager.Instance.StopBackgroundMusic();
            SceneManager.LoadScene("GameOver");
            isGameOverHandled = true;
        }
        
        if(!isGameOverHandled) {

            timeUntilFirstTask -= Time.deltaTime;
            if (!hasGivenTask && timeUntilFirstTask <= 0) {
                NotificationManager.Instance.CreateNotification(3);
                hasGivenTask = true;
            }

            timeUntilNextZoomWindow -= Time.deltaTime;
            if (timeUntilNextZoomWindow <= 0)
            {
                SpawnZoomWindow();
                timeUntilNextZoomWindow = timeBetweenZoomWindows; // Reset timer
            }


            elapsedTime += Time.deltaTime;  // Increment elapsed time by the time since last frame
            UpdateDisplayTime();
            if(isVirus) {
                nextSpawnTime -= Time.deltaTime;
                if (nextSpawnTime <= 0)
                {
                    SpawnEnemy();
                    nextSpawnTime = spawnRate;
                }
            }
            timeUntilNextWindow -= Time.deltaTime;
            if (timeUntilNextWindow <= 0)
            {
                SpawnNotification();
                timeUntilNextWindow = timeBetweenWindows * GetMultiplier(); // Reset timer
            }
        }
    }

    void SpawnEnemy()
    {

        float randomX = Random.Range(spawnArea.rect.min.x, spawnArea.rect.max.x);
        Vector2 spawnPosition = new Vector2(randomX, spawnArea.rect.max.y);
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        newEnemy.transform.SetParent(spawnArea, false);

    }

    private void SpawnNotification() {
        if(Random.Range(0, 100) < 5) {
            NotificationManager.Instance.CreateNotification(3);
        } else {

            int randomIndex = Random.Range(4, WindowManager.Instance.windowConfigs.Count);
            NotificationManager.Instance.CreateNotification(randomIndex);
        }
    }

    private void UpdateDisplayTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        string timeString = timeSpan.ToString(@"mm\:ss");
        timeText.text = "Time in office\n" + timeString;  // Format and update the TMP text every frame
        highScoreString = timeString;
    }

    public float GetMultiplier()
    {
        return (float)Math.Exp(-decayConstant * elapsedTime);
    }

}
