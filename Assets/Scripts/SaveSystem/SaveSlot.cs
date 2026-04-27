using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string profileID;
    [SerializeField] GameObject NewGameText;
    [SerializeField] GameObject LoadGameText;
    [SerializeField] GameObject dayCountText;
    bool gameSave;

    public void SetData(GameData data)
    {
        if (data == null)
        {
            Debug.Log(profileID + " data null");
            NewGameText.SetActive(true);
            LoadGameText.SetActive(false);
            dayCountText.SetActive(false);
            gameSave = false;
        }
        else
        {
            Debug.Log(profileID + " data found");
            NewGameText.SetActive(false);
            LoadGameText.SetActive(true);
            dayCountText.SetActive(true);
            TextMeshProUGUI dayText = dayCountText.GetComponent<TextMeshProUGUI>();
            dayText.text = "Day" + (data.lastDayCompleted+1).ToString();
            gameSave = true;
        }
    }

    public string GetProfileId()
    {
        return this.profileID;
    }

    public void NewGame()
    {
        SaveManager.instance.profileID = profileID;
        MenuManager.instance.StartGame();
    }

    public void LoadGame()
    {
        SaveManager.instance.profileID = profileID;
        SaveManager.instance.LoadGame();
        MenuManager.instance.StartGame();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameSave == true)
        {
            LoadGame();   
        }
        else
        {
            NewGame();
        }
    }
}
