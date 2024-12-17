using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Printer : MonoBehaviour
{
    private Stack<string> prints = new Stack<string>();
    public TextMeshProUGUI printText;
    private float timeBetweenUpdates = 0.8f;
    private float timeUntilNextUpdate = 0f;

    void Start()
    {
        timeUntilNextUpdate = timeBetweenUpdates;
        PopulatePrints();
    }

    void Update()
    {
        timeUntilNextUpdate -= Time.deltaTime;
        if (timeUntilNextUpdate <= 0 && prints.Count > 0)
        {
            timeUntilNextUpdate = timeBetweenUpdates * randomize();
            var textToAdd = prints.Pop();
            printText.text += textToAdd;

            // Check if the popped text is the one that triggers window closure
            if (textToAdd.Trim() == "closing windows...")
            {
                GameManager.Instance.CompleteAllTasks();
            }
        }

        // Destroy the object only when there are no more prints left
        if (prints.Count == 0)
        {
            GetComponent<Window>().CloseWindowAmicably();
        }
    }

    private float randomize() {
        return Random.Range(0.2f, 2.5f);
    }

    private void PopulatePrints()
    {
        prints.Push("\n");
        prints.Push("done. closing...\n");
        prints.Push("cleaning up...\n");
        prints.Push("closing windows...\n");
        prints.Push("\ncompleting tasks...\n");
    }
}
