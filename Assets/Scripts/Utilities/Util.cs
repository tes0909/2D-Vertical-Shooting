using UnityEngine;
using UnityEngine.UI;

public static class Util
{
    public static T InstantiateUI<T>(T prefab, Transform parent) where T : Component
    {
        T instance = GameObject.Instantiate(prefab, parent);
        RectTransform rectTransform = instance.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.rotation = Quaternion.identity;
        }
        return instance;
    }
}
