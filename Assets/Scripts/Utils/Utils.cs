using UnityEngine;

public static class Utils
{

    
    public static int Fib(int n)
    {
        return n == 1 ? 1 : n == 2 ? 1 : Fib(n - 1) + Fib(n - 2);
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return n;
    }

    public static void RemoveAllChilds(Transform parent)
    {
        foreach (Transform child in parent)
            GameObject.Destroy(child.gameObject);
        
    }
    
}