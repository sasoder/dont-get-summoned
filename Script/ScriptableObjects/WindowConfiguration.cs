using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WindowConfiguration", menuName = "Window/Create New Window Configuration")]
public class WindowConfiguration : ScriptableObject
{
    public string windowTitle;
    public Sprite windowIcon;
    public GameObject windowPrefab;  // Prefab for the actual window
    public GameObject taskbarPrefab; // Prefab for the taskbar icon
    public GameObject notificationPrefab; // Prefab for the notification window
    public List<CoworkerConfiguration> coworkerConfiguration; // Configuration for the coworker
    public Vector2 windowSize; // Size of the window
    public float timeForTask; // Time for the task to complete
    public bool isTimed; // Whether the task is timed or not
    public bool spawnsFail;
    public AudioClip spawnSound;
    public AudioClip bgMusic;
}