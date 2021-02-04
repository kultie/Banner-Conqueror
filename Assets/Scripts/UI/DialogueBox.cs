using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    Image avatar;
    [SerializeField]
    Text actorName;
    [SerializeField]
    Text content;
    public static bool finished { private set; get; }
    public static bool started { private set; get; }
    private static DialogueBox Instance;
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public static void Show(string content, string actorName = null, Sprite avatarSprite = null)
    {
        if (Instance.gameObject.activeInHierarchy) return;
        Instance.avatar.gameObject.SetActive(avatarSprite != null);
        Instance.actorName.gameObject.SetActive(actorName != null);
        started = true;
        finished = false;
        if (avatarSprite != null)
        {
            Instance.avatar.sprite = avatarSprite;
        }
        if (actorName != null)
        {
            Instance.actorName.text = actorName;
        }

        Instance.content.text = content;
        Instance.gameObject.SetActive(true);
    }

    public static void Update(float dt)
    {
        if (!Instance.gameObject.activeInHierarchy) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            finished = true;
            Hide();
        }
    }

    public static void Hide()
    {
        started = false;
        Instance.gameObject.SetActive(false);
    }

    public static bool Finished()
    {
        return finished;
    }
}
