using UnityEngine;

[CreateAssetMenu(fileName = "CoworkerConfiguration", menuName = "Coworker/Create New Coworker Configuration")]
public class CoworkerConfiguration : ScriptableObject
{
    public string coworkerName;
    public Sprite coworkerIcon;
    public AudioClip greet;
    public AudioClip thanks;
    public AudioClip sad;
}