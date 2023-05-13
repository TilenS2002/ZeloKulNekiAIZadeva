using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Mood : MonoBehaviour
{
    public Image moodImage;
    public Sprite neutralSprite;
    public Sprite sadSprite;
    public Sprite concernedSprite;
    public Sprite passionSprite;
    public Sprite puzzledSprite;

[SerializeField] private string mood = "neutral";
private void Start()
{
    neutralSprite = Resources.Load<Sprite>("Sprites/neutral");
    sadSprite = Resources.Load<Sprite>("Sprites/sad");
    passionSprite = Resources.Load<Sprite>("Sprites/passion");
    puzzledSprite = Resources.Load<Sprite>("Sprites/puzzled");
    concernedSprite = Resources.Load<Sprite>("Sprites/concerned");
    moodImage = GetComponent<Image>();
}

public void UpdateMood(string moodChange)
{
    mood = moodChange;
    UpdateMoodImage();
    Debug.Log("moodChange:" + moodChange);
}

private void UpdateMoodImage()
{
    Image moodImage;
    moodImage = gameObject.GetComponent<Image>();

    if (mood == "neutral")
    {
        moodImage.sprite = neutralSprite;
    }
    else if (mood == "sad")
    {
        moodImage.sprite = sadSprite;
    }
    else if (mood == "concerned")
    {
        moodImage.sprite = concernedSprite;
    }
    else if (mood == "passion")
    {
        moodImage.sprite = passionSprite;
    }
    else if (mood == "puzzled")
    {
        moodImage.sprite = puzzledSprite;
    }

}
}