using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/**
 * 弹匣
 * */
public class Container : MonoBehaviour
{
    private class ContainerItem
    {
        public System.Guid Id;
        public string Name;
        public int Maximum;

        private int amountTaken; // 已使用弹药数量

        public ContainerItem()
        {
            Id = System.Guid.NewGuid();
        }
        public int Remaining
        {
            get
            {
                return Maximum - amountTaken;
            }
        }

        /**
         * 判断并获取弹药
         * */
        public int Get(int value)
        {
            if((amountTaken + value) > Maximum)
            {
                int tooMuch = amountTaken + value - Maximum;
                amountTaken = Maximum;
                return value - tooMuch;
            }

            amountTaken += value;
            return value;
        }

    }

    List<ContainerItem> items;
    public event System.Action OnContainerReady;

    private void Awake()
    {
        items = new List<ContainerItem>();
        if(OnContainerReady != null)
        {
            OnContainerReady();
        }
    }

    /**
     * 添加弹药
     * */
    public System.Guid Add(string name, int maximum)
    {
        items.Add(new ContainerItem
        {
            Id = System.Guid.NewGuid(),
            Maximum = maximum,
            Name = name
        });

        return items.Last().Id;
    }
    /**
     * 获取弹药
     * */
    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);
        if(containerItem == null)
        {
            return -1;
        }
        return containerItem.Get(amount);
    }

    /**
     * 获取剩余弹药数量
     * */
    public int GetAmountRemaining(System.Guid id)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
        {
            return -1;
        }
        return containerItem.Remaining;
    }

    private ContainerItem GetContainerItem(System.Guid id)
    {
        var containerItem = items.Where(x => x.Id == id).FirstOrDefault();
        if (containerItem == null)
        {
            return null;
        }
        return containerItem;
    }
}
