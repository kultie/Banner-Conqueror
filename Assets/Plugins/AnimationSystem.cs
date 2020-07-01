using UnityEngine;

public class AnimationSystem
{
    Sprite[] frames;
    bool loop;
    float spf;
    float time;
    int index;

    public AnimationSystem(Sprite[] frames, bool loop, float spf = 0.015f)
    {
        this.loop = loop;
        this.frames = frames;
        this.spf = spf;
        time = 0;
        index = 0;
    }

    public void Update(float dt)
    {
        time += dt;
        if (time >= spf)
        {
            index += 1;
            time = 0;
            if (index > frames.Length - 1)
            {
                if (loop)
                {
                    index = 0;
                }
                else
                {
                    index = frames.Length;
                }
            }
        }
    }

    public void SetFrames(Sprite[] frames, bool loop, float spf = 0.015f)
    {
        this.loop = loop;
        this.frames = frames;
        this.spf = spf;
        time = 0;
        index = 0;
    }

    public Sprite Frame()
    {
        var i = index;
        if (i == frames.Length)
        {
            i = frames.Length - 1;
        }
        return frames[i];
    }

    public Sprite[] Frames()
    {
        return frames;
    }

    public bool IsFinished()
    {
        return !loop && index == frames.Length;
    }

    public void ChangeSpeed(float value)
    {
        spf = spf / value;
    }

    public void ResetIndex()
    {
        index = 0;
    }
}