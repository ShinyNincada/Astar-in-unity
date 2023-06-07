using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>{

    T[] items;
    int Count;

    public Heap(int maxHeapSize){
        items = new T[maxHeapSize];
    }

    public void Add(T item){
        item.HeapIndex = Count;
        items[Count] = item;
        SortUp(item);
        Count++;
    }
    
    void SortUp(T item){
        int parentIndex = (item.HeapIndex - 1)/2;

        while(true){
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0) {
                Swap(item, parentItem);
            }
            else{
                break;
            }
        }
    }

    /**Swap the heap index of item A and item B*/
    void Swap(T itemA, T itemB){
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}


public interface IHeapItem<T> : IComparable<T> {
        int HeapIndex {
            get;
            set;
        }
}
