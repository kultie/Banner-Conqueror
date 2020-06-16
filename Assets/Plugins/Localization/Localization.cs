using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadWriteCsv;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Text;
public class Localization : Singleton<Localization>
{
    private Dictionary<string, Dictionary<string, string>> localizationMap = new Dictionary<string, Dictionary<string, string>>();
    private List<string> languages = new List<string>();
    public int currentLanguage = 0;
    private Action<string> onLanguageChange;

    static string languagePref = "language";
    private void Awake()
    {
        base.Awake();
        Load();
    }
    private void Load()
    {
        var header = true;
        if (File.Exists(Path.Combine(Application.persistentDataPath, "language.csv")))
        {
            ReloadTranslate(Path.Combine(Application.persistentDataPath, "language.csv"));
        }
        //else
        //{
        //    var csv = Ftech.Prototype.TemplateResourceManager.GetTextAsset("language");

        //    var stream = new MemoryStream(csv.bytes);
        //    using (stream)
        //    {
        //        CsvFileReader reader = new CsvFileReader(stream, Encoding.UTF8);
        //        CsvRow row = new CsvRow();
        //        while (reader.ReadRow(row))
        //        {
        //            int column = 0;
        //            string key = string.Empty;
        //            foreach (string s in row)
        //            {
        //                if (column == 0)
        //                {
        //                    key = s.Trim();
        //                }
        //                else
        //                {
        //                    if (header)
        //                    {
        //                        localizationMap.Add(s, new Dictionary<string, string>());
        //                        languages.Add(s.Trim());
        //                    }
        //                    else
        //                    {
        //                        var lang = languages[column - 1];
        //                        try
        //                        {
        //                            localizationMap[lang].Add(key, s);
        //                        }
        //                        catch
        //                        {
        //                            //Debug.Log("key :" + key);
        //                        }

        //                    }
        //                }
        //                column++;
        //            }
        //            header = false;
        //        }
        //    }
        //}
        SetLanguage(GetSavedLanguage());
    }

    public void ReloadTranslate(string path)
    {
        languages.Clear();
        localizationMap.Clear();
        var header = true;
        byte[] csv = File.ReadAllBytes(path);
        var stream = new MemoryStream(csv);
        using (stream)
        {
            CsvFileReader reader = new CsvFileReader(stream, Encoding.UTF8);
            CsvRow row = new CsvRow();
            while (reader.ReadRow(row))
            {
                int column = 0;
                string key = string.Empty;
                foreach (string s in row)
                {
                    if (column == 0)
                    {
                        key = s.Trim();
                    }
                    else
                    {
                        if (header)
                        {
                            localizationMap.Add(s, new Dictionary<string, string>());
                            languages.Add(s.Trim());
                        }
                        else
                        {
                            var lang = languages[column - 1];
                            try
                            {
                                localizationMap[lang].Add(key, s);
                            }
                            catch
                            {
                                //Debug.Log("key :" + key);
                            }

                        }
                    }
                    column++;
                }
                header = false;
            }
        }
    }

    public static bool SetLanguage(string lang)
    {
        if (Instance == null)
        {
            Debug.Log("Localization did not init");
            return false;
        }

        int index = Instance.languages.IndexOf(lang);
        if (index < 0)
            return false;

        var lastLang = Instance.languages[(int)Instance.currentLanguage];

        if (lastLang.Equals(lang))
        {
            return true;
        }

        Instance.currentLanguage = index;
        SaveLanguage(lang);

        Instance.onLanguageChange?.Invoke(lang);
        return true;
    }

    public static bool SetLanguage(int lang)
    {
        if (Instance == null)
        {
            Debug.Log("Localization did not init");
            return false;
        }

        int index = lang;
        if (index < 0)
            return false;

        var lastIndex = Instance.currentLanguage;

        if (lastIndex.Equals(index))
        {
            return true;
        }

        Instance.currentLanguage = index;
        SaveLanguage(Instance.languages[index]);

        Instance.onLanguageChange?.Invoke(Instance.languages[index]);
        return true;
    }

    public static string Get(string key, bool format = true)
    {
        if (Instance == null)
        {
            Debug.Log("Localization did not init");
            return string.Empty;
        }

        try
        {
            if (!string.IsNullOrEmpty(key))
            {
                return format ? Regex.Unescape(Instance.localizationMap[Instance.languages[Instance.currentLanguage]][key]) :
                Instance.localizationMap[Instance.languages[Instance.currentLanguage]][key];
            }
            else
            {
                return string.Empty;
            }
        }
        catch
        {
            return string.Empty;
        }

    }

    public static string Get(string key, Dictionary<string, string> dicValue)
    {
        string a = Get(key, true);

        foreach (KeyValuePair<string, string> kv in dicValue)
        {
            a = a.Replace(kv.Key, kv.Value);
        }

        return a;
    }
    public static void AddOnLanguageChange(Action<string> a)
    {
        if (Instance == null)
        {
            Debug.Log("Localization did not init");
            return;
        }

        Instance.onLanguageChange += a;
    }

    public static void RemoveOnLanguageChange(Action<string> a)
    {
        if (Instance == null)
        {
            return;
        }

        Instance.onLanguageChange -= a;
    }

    private static void SaveLanguage(string lang)
    {
        PlayerPrefs.SetString(languagePref, lang);
    }

    public static int GetSavedLanguage()
    {
        string result = PlayerPrefs.GetString(languagePref, "en");
        return Instance.languages.IndexOf(result);
    }

    public static string GetCurrentLangue()
    {
        return Instance.languages[Instance.currentLanguage];
    }

    public static int GetLangIndex(string lang)
    {
        return Instance.languages.IndexOf(lang);
    }
}