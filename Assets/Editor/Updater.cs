using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Updater : ScriptableWizard
{
    public GameObject StandardObject;
    public List<GameObject> objectToReplace;

    [MenuItem("MyTools/ReplaceObjectWithNew")]
    private static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<Updater>("Replace object", "Replace");
    }

    private void OnWizardCreate()
    {
        foreach (GameObject go in objectToReplace)
        {
            Vector3 position = go.transform.position;
            Quaternion rotation = go.transform.rotation;
            Vector3 scale = go.transform.localScale;

            GameObject newGo = Instantiate(StandardObject);

            newGo.transform.position = position;
            newGo.transform.rotation = rotation;
            newGo.transform.localScale = scale;

            DestroyImmediate(go);
        }
    }
}
