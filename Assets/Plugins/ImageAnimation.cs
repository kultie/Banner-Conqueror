using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    public float spf;
    public float delayBetweenLoop;
    public Sprite[] anims;
    public Image image;
    private int currentIndex = 0;
    private float currentTime;
    private float currentDelayTime;
    private void Update()
    {
        float dt = Time.deltaTime;
        image.sprite = anims[currentIndex];
        if (currentDelayTime > 0)
        {
            currentDelayTime -= dt;
            return;
        }
        if (currentTime > 0)
        {
            currentTime -= dt;
        }
        else
        {
            currentIndex = (currentIndex + 1) % anims.Length;
            if (currentIndex == anims.Length - 1)
            {
                currentDelayTime = delayBetweenLoop;
            }
            currentTime = spf;
        }

    }
}
