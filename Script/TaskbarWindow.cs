using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for event handling like clicks
using TMPro;

public class TaskbarWindow : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image iconImage; // Assign in Inspector
    [SerializeField] private Color defaultIconColor; // Assign in Inspector
    [SerializeField] private Color hoverIconColor; // Assign in Inspector
    [SerializeField] private Color hoverColor; // Assign in Inspector
    [SerializeField] private Color defaultColor; // Assign in Inspector
    [SerializeField] private Color minimizedColor; // Assign in Inspector
    private Image bgColorImage; // Assign in Inspector
    private bool isMinimized = false;
    private float timeForTask;
    private float timeLeft;
    private bool isTimed;
    public TextMeshProUGUI panicText;

    private Window linkedWindow; // The window instance this icon represents

    public void Configure(Window linkedWindow)
    {
        bgColorImage = GetComponent<Image>();
        bgColorImage.color = defaultColor;
        this.linkedWindow = linkedWindow;
        iconImage.sprite = linkedWindow.iconImage.sprite; // Set icon from window
        isTimed = linkedWindow.GetConfiguration().isTimed;
        timeForTask = linkedWindow.GetTimeLeft();
        timeLeft = timeForTask;

        // Subscribe to window events
        linkedWindow.OnClosed += HandleWindowClosed;
        linkedWindow.OnMinimized += HandleWindowMinimized;
        linkedWindow.OnRestored += HandleWindowRestored;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (linkedWindow.gameObject.activeSelf)
        {
            linkedWindow.MinimizeWindow();
        }
        else
        {
            linkedWindow.RestoreWindow();
        }
    }

    void Update() {
        if (isTimed) {
            timeLeft = linkedWindow.GetTimeLeft();
            if (timeLeft <= 10) {
                if (!panicText.gameObject.activeSelf) {
                    SoundManager.Instance.PlayTickingSound();
                    panicText.gameObject.SetActive(true);
                }
                if(iconImage.gameObject.activeSelf) {
                    iconImage.gameObject.SetActive(false);
                }
                // Convert the float to an integer string without decimals directly
                panicText.text = timeLeft.ToString("F0");
            }
        }
    }




    public void OnPointerEnter(PointerEventData eventData)
    {
        bgColorImage.color = hoverColor; // Change color on hover
        // Highlight the icon
        iconImage.color = hoverIconColor; // Example: Change color on hover
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isMinimized) {
            bgColorImage.color = defaultColor; // Reset color when not hovering
        } else {
            bgColorImage.color = minimizedColor;
        }
        // Remove highlight
        iconImage.color = defaultIconColor; // Reset color when not hovering
    }

    private void HandleWindowClosed()
    {
        Destroy(gameObject); // Destroy the taskbar icon when the window is closed
    }

    private void HandleWindowMinimized()
    {
        // Optionally change the icon appearance or do nothing
        isMinimized = true;
        bgColorImage.color = minimizedColor;
    }

    private void HandleWindowRestored()
    {
        // Optionally reset the icon appearance or do nothing
        isMinimized = false;
        bgColorImage.color = defaultColor;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (linkedWindow != null)
        {
            linkedWindow.OnClosed -= HandleWindowClosed;
            linkedWindow.OnMinimized -= HandleWindowMinimized;
            linkedWindow.OnRestored -= HandleWindowRestored;
        }
    }
}
