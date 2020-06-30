using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitInterpreter
{
    public static Dictionary<string, ParamsFunc> functionMap = new Dictionary<string, ParamsFunc>()
    {
        {"pause_animation", PauseAnimation },
        {"resume_animation", ResumeAnimation },
        {"play_defined_animation", PlayDefinedAnimation },
        {"play_animation", PlayAnimation },
        {"set_sprite", SetSprite },
        {"is_animation_done", IsAnimationDone },
        {"deal_damage", DealDamage }
    };

    private static object IsAnimationDone(Dictionary<string, object> args)
    {
        UnitEntity entity = (UnitEntity)args["entity"];
        return entity.display.AnimFinished();
    }

    private static object SetSprite(Dictionary<string, object> args)
    {
        UnitEntity entity = (UnitEntity)args["entity"];
        int spriteFrame = (int)args["sprite_index"];
        entity.display.SetSprite(spriteFrame);
        return null;
    }

    private static object PlayDefinedAnimation(Dictionary<string, object> args)
    {
        UnitEntity entity = (UnitEntity)args["entity"];
        string anim_id = (string)args["anim_id"];
        entity.display.RequestAnimation(anim_id);
        return null;
    }

    private static object PlayAnimation(Dictionary<string, object> args)
    {
        UnitEntity entity = (UnitEntity)args["entity"];
        JSONArray animFrames = (JSONArray)args["frames"];
        float spf = (float)args["spf"];
        AnimationData animData = new AnimationData();
        Sprite[] frames = new Sprite[animFrames.Count];
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = entity.data.GetSprite(animFrames[i].AsInt);
        }
        animData.frames = frames;
        animData.spf = spf;
        animData.loop = false;
        entity.display.RequestAnimation(animData);
        return null;
    }

    private static object ResumeAnimation(Dictionary<string, object> args)
    {
        UnitEntity entity = (UnitEntity)args["entity"];
        entity.display.ResumeAnimation();
        return null;
    }

    private static object PauseAnimation(Dictionary<string, object> args)
    {
        UnitEntity entity = (UnitEntity)args["entity"];
        entity.display.PauseAnimation();
        return null;
    }

    private static object DealDamage(Dictionary<string, object> args)
    {
        UnitEntity entity = (UnitEntity)args["entity"];
        entity.TakeDamage((float)args["damage"]);
        return null;
    }
}
