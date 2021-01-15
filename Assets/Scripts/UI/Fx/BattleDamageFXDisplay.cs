using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDamageFXDisplay : FXEntityDisplay<BattleDamageFXEntity>
{
    [SerializeField]
    Sprite[] textSprites;
    [SerializeField] SpriteRenderer textRenderer;
    List<SpriteRenderer> renders = new List<SpriteRenderer>();
    string values;
    float currentTime;
    protected override void Initialized(BattleDamageFXEntity entity)
    {
        currentTime = 0;
        values = entity.value.ToString();
        while (renders.Count < values.Length)
        {
            renders.Add(Instantiate(textRenderer, transform));
        }
        for (int i = 0; i < values.Length; i++)
        {
            renders[i].sprite = textSprites[int.Parse(values[i].ToString())];
            renders[i].transform.localPosition = new Vector2(i * 0.1f, 0);
            renders[i].gameObject.SetActive(true);
        }
    }
    protected override void InternalOnDisable()
    {
        renders.ForEach(r => r.gameObject.SetActive(false));
    }
    protected override void OnUpdate(float dt)
    {
        currentTime += dt;
        for (int i = 0; i < values.Length; i++)
        {
            renders[i].transform.localPosition = new Vector2(i * 0.1f, Mathf.Round(Mathf.Sin(currentTime * 5 + i * 0.2f) * 96 * 0.1f) / 96);
        }
        entity.Update(dt);
        if (currentTime >= 2) {
            gameObject.Recycle();
        }
    }
}
