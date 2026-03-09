using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Scriptable Objects/Achievements")]
public class Achievements : ScriptableObject
{
    public string achievementName;
    public string requirements;
    public bool achieved;
    public string reward;
}
