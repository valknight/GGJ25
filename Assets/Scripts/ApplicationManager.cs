using System.Collections.Generic;
using System.IO;
using Data;
using Market_Simulation;
using UnityEngine;

public static class ApplicationManager
{
    public static Dictionary<string, ApplicationDefinition> ApplicationDefinitions;
    public static Dictionary<GameObject, ApplicationDefinition> ActiveApplications;

    public static void Init()
    {
        ApplicationDefinitions = new Dictionary<string, ApplicationDefinition>();
        var data = Resources.LoadAll<ApplicationDefinition>("ApplicationData");
        foreach (var definition in data)
        {
            ApplicationDefinitions.Add(definition.applicationId, definition);
        }
    }

    public static bool TryGetDefinition(string id, out ApplicationDefinition def)
    {
        return ApplicationDefinitions.TryGetValue(id, out def);
    }

    public static bool TryGetWindowForApplication(string id, out GameObject window)
    {
        window = null;
        if (!TryGetDefinition(id, out var def)) return false;

        window = GameObject.Find(def.applicationWindow.name);
        return window != null;
    }
}
