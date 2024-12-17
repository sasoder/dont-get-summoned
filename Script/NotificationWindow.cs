using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class NotificationWindow : MonoBehaviour
{

    public TextMeshProUGUI sender; // Assign in Inspector
    public TextMeshProUGUI messageText; // Assign in Inspector
    private int windowConfigIndex; // Index to spawn the specific window related to this notification
    private WindowConfiguration windowConfig; // Reference to the window configuration
    private List<string> notificationMessages = new List<string>();
    public Image coworkerImage;
    private CoworkerConfiguration coworker;
    public RectTransform timer;
    private float remainingTime;
    private float totalTime;


    void Awake()
    {
        PopulateNotificationMessages();
    }

    public void Initialize(int configIndex)
    {
        if(configIndex >= 0 && configIndex < WindowManager.Instance.windowConfigs.Count) {
            windowConfig = WindowManager.Instance.windowConfigs[configIndex];
            windowConfigIndex = configIndex;
            messageText.text = notificationMessages[windowConfigIndex];
            coworker = getCoworker();
            sender.text = coworker.coworkerName;
            coworkerImage.sprite = coworker.coworkerIcon;
            totalTime = 10f;
            remainingTime = totalTime;
            SoundManager.Instance.PlaySound(coworker.greet);
        }
    }

    private CoworkerConfiguration getCoworker() {
        return windowConfig.coworkerConfiguration[Random.Range(0, windowConfig.coworkerConfiguration.Count)];
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;

        // Calculate the new width, decreasing from 270 to 0
        float newWidth = Mathf.Lerp(270, 0, 1 - remainingTime / totalTime);
        timer.sizeDelta = new Vector2(newWidth, timer.sizeDelta.y);

        // Calculate the new posX, increasing from 0 to 135
        float newPosX = Mathf.Lerp(0, 135, 1 - remainingTime / totalTime);
        timer.anchoredPosition = new Vector2(newPosX, timer.anchoredPosition.y);

        // Check if time has run out
        if (remainingTime <= 0) {
            GameManager.Instance.onTaskFail(false);
            Destroy(gameObject);
        }
    }

    // Method to be called when the notification is clicked
    public void OnNotificationClicked()
    {
        if(windowConfigIndex == 3) {
            GameManager.Instance.SpawnPythonProgram();
        } else {
        // Spawn the related window
        Window relatedWindow = WindowManager.Instance.SpawnWindow(windowConfigIndex, coworker);
        TaskManager.Instance.SpawnTaskbarWindow(relatedWindow);
        }
        // Optionally close the notification
        Destroy(gameObject);
    }

    private void SpawnPythonProgram() {
    }

    private void PopulateNotificationMessages() {
        // Variations of default notification messages you may get at work
        notificationMessages.Add("placeholder");
        notificationMessages.Add("placeholder");
        notificationMessages.Add("placeholder");
        notificationMessages.Add("Added a script to your desktop, helps you with your work!");
        notificationMessages.Add("Could you please help me solve this captcha?");
        notificationMessages.Add("Hey, I just sent you a message!");
        notificationMessages.Add("I need help with writing an article. Can you help?");
        notificationMessages.Add("I clicked this link I got in an e-mail, now my computer's acting up!");
        notificationMessages.Add("Need your help with locating a country, asap!");
        notificationMessages.Add("New message!");
    }
}
