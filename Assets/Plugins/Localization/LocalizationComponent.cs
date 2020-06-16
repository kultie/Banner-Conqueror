using System;
using System.Collections.Generic;
using UnityEngine;
namespace Ftech.Utilities
{
    public abstract class LocalizationComponent : MonoBehaviour
    {
        [SerializeField]
        protected string key;

        protected abstract void OnChangeLanguage(string lang);

        protected virtual void OnEnable()
        {
            Localization.AddOnLanguageChange(OnChangeLanguage);
        }

        protected virtual void OnDisable()
        {
            Localization.RemoveOnLanguageChange(OnChangeLanguage);
        }

        public void SetKey(string val)
        {
            key = val;
        }

        public void UpdateLangage()
        {
            OnChangeLanguage(Localization.GetCurrentLangue());
        }
    }
}