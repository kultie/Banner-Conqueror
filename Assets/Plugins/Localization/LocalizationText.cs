using System;
using UnityEngine.UI;
using Ftech.Utilities;

public class LocalizationText : LocalizationComponent
{
    private Text text;
    public bool RegexEscape = true;
    public Action<string> OnChangeLanguageCallBack;

    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
    }

    protected override void OnChangeLanguage(string lang)
    {
        if (this != null)
        {
            string _text = Localization.Get(key, RegexEscape);
            if (_text != "")
            {
                if (text != null)
                {
                    text.text = _text;
                }
                OnChangeLanguageCallBack?.Invoke(_text);
            }
        }
    }
    protected override void OnEnable()
    {
        string _text = Localization.Get(key, RegexEscape);
        if (_text != "")
        {
            if (text != null)
            {
                text.text = _text;
            }
            OnChangeLanguageCallBack?.Invoke(_text);
        }
        base.OnEnable();
    }
}

