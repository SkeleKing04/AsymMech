using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StatEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    enum SupportedTypes
    {
        FLT = 0,
    };
    private static string m_lastSelectedFile;
    private Dictionary<string, StatBlock> m_loadedStats;
    private VisualElement[] statEntryFields = new VisualElement[0]; 

    [MenuItem("Tools/Stat Editor")]
    public static void ShowExample()
    {
        StatEditor wnd = GetWindow<StatEditor>();
        wnd.titleContent = new GUIContent("StatEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        root.Clear();

        DrawButtons(root);

        // Instantiate UXML
        //VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        //root.Add(labelFromUXML);
    }
    private void DrawButtons(VisualElement root)
    {
        TextField filePathField = new TextField("File path");
        filePathField.textEdition.placeholder = "Input path";
        if(m_lastSelectedFile != "")
            filePathField.value = m_lastSelectedFile;
        root.Add(filePathField);

        Button filePathLoadButton = new Button() {text = "Load File"};
        filePathLoadButton.clicked += () =>
        {
            if(AttemptLoadFile(filePathField.text))
            {
                CreateGUI();
                root.Add(DrawDic(filePathField.text));
            }
        };
        root.Add(filePathLoadButton);
    }
    private bool AttemptLoadFile(string path)
    {
            if(path.EndsWith(".csv"))
            if(File.Exists(path))
            {
                Debug.Log($"File {path} found!");
                m_lastSelectedFile = path;
                return true;
            }
            else Debug.LogError($"No file found at {path}");
            else Debug.LogWarning($"Incorrect file format (should be .csv)");

            return false;
    }
    private VisualElement DrawDic(string csvPath)
    {
        VisualElement container = new VisualElement();
        container.style.paddingTop = 5;

        var linesOut = File.ReadLines(csvPath);
        //save everything to the dic for ez edits
        m_loadedStats = new Dictionary<string, StatBlock>();
        foreach(var line in linesOut)
        {
            string[] values = line.Split(',');
            m_loadedStats.Add(values[0], new StatBlock(0, float.Parse(values[1]), float.Parse(values[2])));
        }
        statEntryFields = new TextField[linesOut.Count() * 3];

        //titles
        VisualElement titles = new VisualElement();
        titles.style.alignSelf = Align.FlexStart;
        titles.style.flexDirection = FlexDirection.Row;
        Label nameLabel = new Label("Entry Name");
        nameLabel.style.width = 200;
        nameLabel.style.paddingLeft = 5;
        titles.Add(nameLabel);
        Label lowLabel = new Label("Low Cap");
        lowLabel.style.width = 70;
        titles.Add(lowLabel);
        Label highLabel = new Label("High Cap");
        highLabel.style.width = 70;
        titles.Add(highLabel);
        container.Add(titles);

        //entries
        List<VisualElement> newFields = new List<VisualElement>();
        foreach(var dataEntry in m_loadedStats)
        {
            VisualElement entry = new VisualElement();
            entry.style.alignSelf = Align.FlexStart;
            entry.style.flexDirection = FlexDirection.Row;
            TextField entryNameField = new TextField();
            entryNameField.value = dataEntry.Key;
            entryNameField.style.width = 200;
            entry.Add(entryNameField);
            newFields.Add(entryNameField);

            FloatField entryFlagLowValue = new FloatField();
            entryFlagLowValue.value = dataEntry.Value.lowCap;
            entryFlagLowValue.style.width = 70;
            entry.Add(entryFlagLowValue);
            newFields.Add(entryFlagLowValue);

            FloatField entryFlagHighValue = new FloatField();
            entryFlagHighValue.value = dataEntry.Value.highCap;
            entryFlagHighValue.style.width = 70;
            entry.Add(entryFlagHighValue);
            newFields.Add(entryFlagHighValue);

            container.Add(entry);
        }
        statEntryFields = newFields.ToArray();

        Button filePathSaveButton = new Button() {text = "Save Data"};
        filePathSaveButton.clicked += () =>
        {
            StreamWriter sw = new StreamWriter(m_lastSelectedFile);
            for(int i = 0; i < statEntryFields.Length; i += 3)
            {
                sw.WriteLine($"{(statEntryFields[i] as TextField).value},{(statEntryFields[i+1] as FloatField).value},{(statEntryFields[i+2] as FloatField).value}");
            }
            sw.Close();
        };
        container.Add(filePathSaveButton);

        return container;
    }
}
