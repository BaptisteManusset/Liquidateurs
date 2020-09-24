using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Tools
{
    public static void AddElementTocollect(this GameObject value)
    {
        if (!GameManager.ElementsToCollect.Contains(value))
            GameManager.ElementsToCollect.Add(value);
    }
    public static void RemoveElementTocollect(this GameObject value)
    {
        if (GameManager.ElementsToCollect.Contains(value))
            GameManager.ElementsToCollect.Remove(value);
    }

    public static List<GameObject> FindNearestObject(List<GameObject> list, GameObject origin)
    {
        List<GameObject> results = list.OrderBy(x => Vector3.Distance(origin.transform.position, x.transform.position)).ToList();
        return results;
    }


}
