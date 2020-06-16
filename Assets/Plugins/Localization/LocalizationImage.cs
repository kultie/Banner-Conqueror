using UnityEngine;
using UnityEngine.UI;
using Ftech.Utilities;
public class LocalizationImage : LocalizationComponent
{
    private Image image;
    bool RegexEscape = true;

    [SerializeField]
    Sprite[] sprites;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    protected override void OnChangeLanguage(string lang)
    {
        string src = Localization.Get(key, RegexEscape);
        image.sprite = sprites[Localization.GetLangIndex(lang)];
        image.SetNativeSize();
    }

    protected override void OnEnable()
    {
        string lang = Localization.GetCurrentLangue();
        image.sprite = sprites[Localization.GetLangIndex(lang)];
        image.SetNativeSize();
        base.OnEnable();
    }

    //[SerializeField]
    //int imageIndex;
    //protected override void OnChangeLanguage(string lang)
    //{
    //    string src = Localization.Get(key,RegexEscape);
    //    image.sprite = Resources.LoadAll<Sprite>(src)[imageIndex];
    //    image.SetNativeSize();
    //}

    //protected override void OnEnable()
    //{
    //    string src = Localization.Get(key, RegexEscape);
    //    image.sprite = Resources.LoadAll<Sprite>(src)[imageIndex];
    //    image.SetNativeSize();
    //    base.OnEnable();
    //}
}
