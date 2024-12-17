using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Window : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText; // Assign in Inspector
    [SerializeField] public Image iconImage; // Assign in Inspector
    [SerializeField] private Button closeButton; // Assign in Inspector
    [SerializeField] private Button minimizeButton; // Assign in Inspector
    private WindowConfiguration windowConfiguration;

    public event System.Action OnClosed; // Event to signal window closing
    public event System.Action OnMinimized; // Event to signal window minimizing
    public event System.Action OnRestored; // Event to signal window restoring
    private float timeForTask;
    private bool isTimed;
    private bool spawnsFail;
    public Image border;
    private CoworkerConfiguration coworker;
    private AudioSource bgMusicSource;
    private AudioSource spawnSoundSource;

    // Configure the window based on a ScriptableObject configuration
    public void Configure(WindowConfiguration config, CoworkerConfiguration coworker)
    {
        if(coworker != null) {
            this.coworker = coworker;
        }
        this.windowConfiguration = config;
        titleText.text = config.windowTitle; // Set the window title
        iconImage.sprite = config.windowIcon; // Set the window icon
        timeForTask = config.timeForTask;
        isTimed = config.isTimed;
        spawnsFail = config.spawnsFail;
        if(closeButton != null) {
            closeButton.onClick.AddListener(() =>CloseWindow(false));
        }

        if(minimizeButton != null) {    
            minimizeButton.onClick.AddListener(MinimizeWindow);
        }
    }

    void Start()
    {
        if (windowConfiguration.spawnSound != null)
        {
            spawnSoundSource = gameObject.AddComponent<AudioSource>();
            spawnSoundSource.clip = windowConfiguration.spawnSound;
            spawnSoundSource.spatialBlend = 0;  // Make it a global sound.
            spawnSoundSource.Play();
        }

        if(windowConfiguration.bgMusic != null) {
            bgMusicSource = gameObject.AddComponent<AudioSource>();
            bgMusicSource.loop = true;  // Set the background music to loop.
            bgMusicSource.clip = windowConfiguration.bgMusic;  // Assign the clip.
            bgMusicSource.spatialBlend = 0;  // Make it a global sound.
            bgMusicSource.Play();
        }
    }

    void Update()
    {
        timeForTask -= Time.deltaTime;

        if (timeForTask <= 0 && isTimed) {
            CloseWindow(spawnsFail);
        }

        if(timeForTask <= 10 && isTimed) {
            // change colour of border gameobject image color
            border.color = Color.red;
        }
    }

    public float GetTimeLeft() {
        return timeForTask;
    }

    public void UpdateWindowTitle(string title) {
        titleText.text = title;
    }

    public void CloseWindow(bool spawnTaskFail) 
    {
        SoundManager.Instance.PlaySound(coworker.sad);
        OnClosed?.Invoke();
        GameManager.Instance.onTaskFail(spawnTaskFail);
        Destroy(gameObject); // Destroys the window GameObject
    }

    public void CloseWindowVirus() 
    {
        SoundManager.Instance.PlaySound(coworker.sad);
        OnClosed?.Invoke();
        GameManager.Instance.onTaskFailVirus();
        Destroy(gameObject); // Destroys the window GameObject
    }

    public void CloseWindowPsycho(bool instaDeath) {
        SoundManager.Instance.PlaySound(coworker.sad);
        OnClosed?.Invoke();
        GameManager.Instance.onTaskFailPsycho(instaDeath);
        Destroy(gameObject); // Destroys the window GameObject
    }

    public void MinimizeWindow()
    {
        OnMinimized?.Invoke();
        gameObject.SetActive(false); // Minimizes the window
    }

    private void HandleRestoreRequested()
    {
        RestoreWindow();
    }

    // Optionally, implement a Restore function if necessary
    public void RestoreWindow()
    {
        // Logic to restore the window, e.g., make it active, bring to front
        gameObject.SetActive(true);
        OnRestored?.Invoke(); // Optionally notify that the window is 'active' or 'restored'.
        BringToFront();
    }

    public void OnWindowClicked()
    {
        BringToFront(); // This will bring the window to the front.
        OnRestored?.Invoke(); // Optionally notify that the window is 'active' or 'restored'.
    }

    private void BringToFront()
    {
        GetComponent<RectTransform>().SetAsLastSibling(); // Brings the window to the front
    }

    public WindowConfiguration GetConfiguration()
    {
        return this.windowConfiguration;
    }

    public CoworkerConfiguration GetCoworkerConfiguration()
    {
        return this.coworker;
    }

    public void CloseWindowAmicably() {
        OnClosed?.Invoke();
        SoundManager.Instance.PlayCloseSound();
        Destroy(gameObject); // Destroys the window GameObject
    }

    public void CompleteTask() {
        SoundManager.Instance.PlaySound(coworker.thanks);
        OnClosed?.Invoke();
        GameManager.Instance.onTaskSuccess();
        Destroy(gameObject); // Destroys the window GameObject
    }

    public void CompleteTaskMap(float score) {
        SoundManager.Instance.PlaySound(coworker.thanks);
        OnClosed?.Invoke();
        GameManager.Instance.onTaskSuccessMap(score);
        Destroy(gameObject); // Destroys the window GameObject
    }

    public void CompleteTaskScript() {
        OnClosed?.Invoke();
        GameManager.Instance.onTaskSuccess();
        Destroy(gameObject); // Destroys the window GameObject
    }

}
