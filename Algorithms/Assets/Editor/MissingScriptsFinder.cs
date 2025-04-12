using UnityEngine;
using UnityEditor;

public class MissingScriptsFinder : MonoBehaviour
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    static void FindMissingScripts()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int missingCount = 0;

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script found on GameObject: '{go.name}' in hierarchy path: {GetGameObjectPath(go)}", go);
                    missingCount++;
                }
            }
        }

        if (missingCount == 0)
        {
            Debug.Log(" No missing scripts found in the scene.");
        }
        else
        {
            Debug.LogWarning($" Total missing scripts: {missingCount}");
        }
    }

    static string GetGameObjectPath(GameObject obj)
    {
        string path = obj.name;
        Transform current = obj.transform;

        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }

        return path;
    }
}
