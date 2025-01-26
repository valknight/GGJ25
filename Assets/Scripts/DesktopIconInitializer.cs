using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DesktopIconInitializer : MonoBehaviour
{
    public DesktopIcon desktopIconPrefab;

    private void Start()
    {
        GetComponent<GridLayoutGroup>().enabled = true;
        foreach (var def in ApplicationManager.ApplicationDefinitions)
        {
            Instantiate(desktopIconPrefab, transform).Init(def.Value);
        }

        StartCoroutine(DisableGrid());
    }

    private IEnumerator DisableGrid()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<GridLayoutGroup>().enabled = false;
    }
}
