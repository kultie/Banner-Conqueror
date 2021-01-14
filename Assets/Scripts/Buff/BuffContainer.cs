using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffContainer
{
    UnitEntity owner;
    private Dictionary<string, BuffBase> buffs;
    List<BuffBase> buffList;
    public BuffContainer(UnitEntity entity)
    {
        owner = entity;
        buffs = new Dictionary<string, BuffBase>();
    }

    public void AddBuff(BuffBase buff)
    {
        bool willBeAdd = true;
        if (buffs.ContainsKey(buff.id))
        {
            if (buff.order > buffs[buff.id].order)
            {
                willBeAdd = false;
            }
        }

        if (willBeAdd)
        {
            buffs[buff.id] = buff;
            buff.OnAdd(owner);
        }
        buffList = buffs.Values.OrderBy(b => b.order).ToList();
    }

    public void RemoveAllBufF()
    {
        buffs.Clear();
        buffList.Clear();
    }

    public BuffBase RemoveBuff(BuffBase buff)
    {
        if (buffs.ContainsKey(buff.id))
        {
            buffs.Remove(buff.id);
            buff.OnRemove(owner);
        }
        buffList = buffs.Values.OrderBy(b => b.order).ToList();
        return buff;
    }

    public void RemoveBuff(int number)
    {
        if (number > buffList.Count)
        {
            RemoveAllBufF();
        }
        else
        {
            int i = 0;
            while (i < number)
            {
                BuffBase b = buffList[i];
                if (buffs.ContainsKey(b.id))
                {
                    buffs.Remove(b.id);
                    b.OnRemove(owner);
                }               
            }
            buffList = buffs.Values.OrderBy(b => b.order).ToList();
        }
    }

    public void ProcessTurn()
    {
        buffList.ForEach(b =>
        {
            if (b.Expired())
            {
                RemoveBuff(b);
            }
            else
            {
                b.ProcessTurn(owner);
            }
        });

    }

    public List<BuffBase> GetBuffs()
    {
        return buffList;
    }
}
