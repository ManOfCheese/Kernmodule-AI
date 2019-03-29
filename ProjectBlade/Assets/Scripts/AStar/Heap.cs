using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapItem<T> {

    T[] items;
    int currenItemCount;

    //Constructor
    public Heap(int maxHeapSize) {
        //Initializes the items array with a size of maxHeapSize as passed into the constructor.
        items = new T[maxHeapSize];  
    }

    //Adds and item to the heap.
    public void Add(T item) {
        //The heap index is set to be equal to the current item count because it is the newest item.
        item.HeapIndex = currenItemCount;
        //The item is assigned to the array at the index of currentItemCount.
        items[currenItemCount] = item;
        //Execute SortUp to get it into the correct position in the heap.
        SortUp(item);                      
        currenItemCount++;
    }

    //Removes the first item from the heap.
    public T RemoveFirst() {
        //Create a variable of type T which is a reference to the first item, therefore index 0.
        T firstItem = items[0];
        currenItemCount--;
        //The last item of the heap is moved to index 0 to replace the first item.
        items[0] = items[currenItemCount];
        items[0].HeapIndex = 0;
        //Execute SortDown to get it into the correct position in the heap.
        SortDown(items[0]);                 
        return firstItem;                 
    }

    //Updates item by executing SortUp on it.
    public void UpdateItem(T item) {
        SortUp(item);
    }

    //Returns current amount of items in the heap.
    public int Count {
        get {
            return currenItemCount;
        }
    }

    //Checks wether the heap contains a certain item.
    public bool Contains(T item) {
        return Equals(items[item.HeapIndex], item);
    }

    //Sorts a value high in the heap downwards by looking at its children.
    void SortDown(T item) {
        while (true) {
            //Calculate the index of the left and right child.
            int childIndexLeft = item.HeapIndex * 2 + 1;   
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            //If the index of the left child is less than the current item count
            if (childIndexLeft < currenItemCount) {
                //it needs to be swapped.
                swapIndex = childIndexLeft;

                //If however the index of the right child is also less we need to check which has priority.
                if (childIndexRight < currenItemCount) {
                    //If the fCost of child index right is lower than that of child index left 
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) {
                        //change swap index to childIndexRight.
                        swapIndex = childIndexRight;                                      
                    }
                }

                //If the item being sorted has a higher fCost than the item indicated by the swap index.
                if (item.CompareTo(items[swapIndex]) < 0) {
                    //Swap 'em.
                    Swap(item, items[swapIndex]);                                         
                }
                else {
                    return;
                }
            }
            //If the index of the left child is not less than currentItemCount.
            else {
                return;
            }
        }
    }
	
    //Sorts items up by looking at their parent.
    void SortUp(T item) {
        //Calculates the index of the parent.
        int parentIndex = (item.HeapIndex - 1) / 2;  

        while (true) {
            //parenItem is the item contained in the array at the index just calculated.
            T parentItem = items[parentIndex];
            //Compare the item to it's parent, if it's fCost is lower.
            if (item.CompareTo(parentItem) > 0) {
                Swap(item, parentItem);
            }
            else {
                break;
            }

            //Recalculate parentIndex after the swap.
            parentIndex = (item.HeapIndex - 1) / 2;  
        }
    }

    //Swaps two items in the heap.
    void Swap(T itemA, T itemB) {

        items[itemA.HeapIndex] = itemB;     
        items[itemB.HeapIndex] = itemA;
        //Temporarily stores the heapIndex for itemA.
        int itemAIndex = itemA.HeapIndex;
        //Executes the swap using the stored heapIndex.
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

//Interface to force inheritors to implement the heapIndex integer.
public interface IHeapItem<T> : IComparable<T> {
    int HeapIndex {
        get;
        set;
    }
}
