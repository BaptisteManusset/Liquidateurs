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


    public static List<GameObject> FindNearestsObjects(List<GameObject> list, GameObject origin)
    {
        return list.OrderBy(x => Vector3.Distance(origin.transform.position, x.transform.position)).ToList();
    }

    public static GameObject FindNearestObject(List<GameObject> list, GameObject origin)
    {
        List<GameObject> results = list.OrderBy(x => Vector3.Distance(origin.transform.position, x.transform.position)).ToList();


        return results.Count > 0 ? results[0] : null;
    }

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
