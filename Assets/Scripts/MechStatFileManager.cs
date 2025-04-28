using UnityEngine;
using System.IO;

public static class MechStatFileManager
{
    static string m_selectedFilePath;

    static void ReadFromPath(string path)
    {
        if(File.Exists(path))
        {
        }
        else Debug.LogError($"No file found at selected path ({path})");
    }
    static void ReloadRead()
    {
        ReadFromPath(m_selectedFilePath);
    }
    static void SelectFile(string path)
    {
        if(File.Exists(path))
        {
            m_selectedFilePath = path;
            Debug.Log($"File {path} selected");
            ReadFromPath(path);
        }
    }
}
