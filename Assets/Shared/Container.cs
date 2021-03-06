﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/**
 * 弹药包，存储所有弹药
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

        internal void Set(int amount)
        {
            amountTaken -= amount;
            if(amountTaken < 0)
            {
                amountTaken = 0;
            }
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

    /// <summary>
    /// 给指定类型弹药设置初始化数量
    /// </summary>
    /// <param name="name"></param>
    /// <param name="maximum"></param>
    /// <returns></returns>
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
     * 增加弹药
     * @param name: 弹药名称
     * @param amount: 弹药数量
     * */
    public void Put(string name, int amount)
    {
        Debug.Log("向Inventory添加弹药:" + name);
        var containerItem = items.Where(x => x.Name == name).FirstOrDefault();
        if(containerItem == null)
        {
            return;
        }
        //Debug.Log("添加弹药前:" + containerItem.Name + containerItem.Id + "有弹药量：" + containerItem.Remaining + ", 已使用弹药量：" + containerItem.amountTaken);
        containerItem.Set(amount);
        //Debug.Log("添加弹药后:" + containerItem.Name + containerItem.Id + "有弹药量：" + containerItem.Remaining + ", 已使用弹药量：" + containerItem.amountTaken);
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
