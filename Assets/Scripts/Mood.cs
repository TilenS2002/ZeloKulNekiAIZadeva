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
    public Sprite laughingSprite;
    public Sprite shockedSprite;

[SerializeField] private string mood = "neutral";
private void Start()
{
    neutralSprite = Resources.Load<Sprite>("Sprites/neutral");
    sadSprite = Resources.Load<Sprite>("Sprites/sad");
    passionSprite = Resources.Load<Sprite>("Sprites/passion");
    puzzledSprite = Resources.Load<Sprite>("Sprites/puzzled");
    concernedSprite = Resources.Load<Sprite>("Sprites/concerned");
    laughingSprite = Resources.Load<Sprite>("Sprites/laugh");
    shockedSprite = Resources.Load<Sprite>("Sprites/shocked");
    moodImage = GetComponent<Image>();
}

public void UpdateMood(string moodChange)
{
    mood = moodChange.ToLower();
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
    else if (mood == "laughing")
    {
        moodImage.sprite = laughingSprite;
    }
    else if (mood == "shocked")
    {
        moodImage.sprite = shockedSprite;
    }

}
}