using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    public Achievements achievement;
    public TextMeshProUGUI achievementName;
    public TextMeshProUGUI requirements;
    public TextMeshProUGUI reward;

    void Start()
    {
        achievementName.text = achievement.achievementName;
        requirements.text = achievement.requirements;
        reward.text = achievement.reward;
        gameObject.name = achievement.achievementName.ToString();
    }

}
