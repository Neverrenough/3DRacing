using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIRaceButton : UISelectableButton
{
    [SerializeField] private RaceInfo raceInfo;
    [SerializeField] private Image icon;
    [SerializeField] private Text title;

    private void Start()
    {
        ApplyProperty(raceInfo);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (raceInfo == null) return;
        SceneManager.LoadScene(raceInfo.SceneName);
    }
    private void ApplyProperty(RaceInfo property)
    {
        if (property == null) return;
        raceInfo = property;

        icon.sprite = raceInfo.Sprite;
        title.text = raceInfo.Title;
    }
}
