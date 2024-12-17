using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ZoomProgram : MonoBehaviour
{

    public TextMeshProUGUI notificationText;
    private float totalTime;
    private float remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        totalTime = 10f;
        remainingTime = totalTime;
    }
    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        notificationText.text = FormatText(remainingTime);
    }

    private string FormatText(float time)
    {
        string msg = "You've been summoned to a Soom meeting. Open the Soom program on your desktop to continue. You have {0} seonds.";
        return string.Format(msg, time);
    }

    public void OnProgramClicked()
    {
        Draggable draggableComponent = GetComponent<Draggable>();

        if (draggableComponent != null && draggableComponent.WasRecentlyDragged())
        {
            return; // Do nothing if the object was recently dragged
        }
        RunScript(); // Execute the script if not recently dragged
    }

    private void RunScript()
    {
        GameManager.Instance.SpawnZoomWindow(); // Your custom function
    }
}
