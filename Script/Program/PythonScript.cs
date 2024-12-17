using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PythonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
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
        GameManager.Instance.SpawnPythonScriptWindow(); // Your custom function
        Destroy(gameObject); // Destroy the object
    }
}
