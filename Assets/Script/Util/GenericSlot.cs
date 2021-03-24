using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class GenericSlot<TData, TSlot> : MonoBehaviour where TData : class where TSlot : GenericSlot<TData, TSlot>
{
    public class Grid
    {
        public List<TSlot> SlotList { get; private set; }

        private Func<List<TData>, List<TSlot>> onCraete = null;
        private Action<TSlot> onDestroy = null;

        public Grid(Func<List<TData>, List<TSlot>> onCraete, Action<TSlot> onDestroy)
        {
            this.onCraete = onCraete;
            this.onDestroy = onDestroy;

            SlotList = new List<TSlot>();
        }

        public void ApplyList(List<TData> dataList)
        {
            var modelCount = dataList.Count;
            var removeCount = SlotList.Count - modelCount;
            if (removeCount > 0)
            {
                for (var i = modelCount; i < SlotList.Count; ++i)
                {
                    onDestroy(SlotList[i]);
                }
                SlotList.RemoveRange(modelCount, removeCount);
            }

            for (var i = 0; i < SlotList.Count; ++i)
            {
                SlotList[i].SetData(dataList[i]);
            }

            if (removeCount >= 0) return;
            var newDataList = dataList.GetRange(SlotList.Count, -removeCount);
            SlotList.AddRange(onCraete(newDataList));
        }
    }

    public TData Data { get; private set; }

    public void SetData(TData data, bool compRef = true)
    {
        if (compRef && Data == data)
            return;

        Data = data;
        OnSetData(data);
    }

    public void Invalidate()
    {
        OnSetData(Data);
    }

    protected abstract void OnSetData(TData data);
}
