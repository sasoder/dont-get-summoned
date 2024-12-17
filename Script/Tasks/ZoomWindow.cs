using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseWindow() {
        GetComponent<Window>().CloseWindowAmicably();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
