using System.Collections.Generic;

public class LoopedList<T>
{
    private List<T> mainList;

    private int curIndex = -1;

    public LoopedList(T[] inputs)
    {
        mainList = new List<T>(inputs);
    }

    public void Add(T objectToAdd)
    {
        mainList.Add(objectToAdd);
    }

    public T GetNext()
    {
        curIndex++;
        if (mainList.Count < curIndex)
            curIndex = 0;

        return mainList[curIndex];
    }
}
