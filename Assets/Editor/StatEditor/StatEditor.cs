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
    //for use later
    enum SupportedTypes
    {
        FLT = 0,
    };
    //for edit refs
    private static string m_lastSelectedFile;
    //for ez editing
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

        if(m_loadedStats != null)
            root.Add(DrawDic());
    }
    private void DrawButtons(VisualElement root)
    {
        //Path field
        TextField filePathField = new TextField("File path");
        filePathField.textEdition.placeholder = "Input path";
        //if there was once a file selected, auto fill it - i don't think this works as the variable gets reset...
        if(m_lastSelectedFile != "")
            filePathField.value = m_lastSelectedFile;
        root.Add(filePathField);

        //This will attempt to load a file when clicked
        Button filePathLoadButton = new Button() {text = "Load File"};
        filePathLoadButton.clicked += () =>
        {
            if(AttemptLoadFile(filePathField.text))
            {
                //Get the file info
                LoadDic(filePathField.text);
                //Reload the GUI
                CreateGUI();
            }
        };
        root.Add(filePathLoadButton);
    }
    /// <summary>
    /// Attempts to load a file from a given path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private bool AttemptLoadFile(string path)
    {
        //Check if it is a real csv file
        if(path.EndsWith(".csv") && File.Exists(path))
        {
            Debug.Log($"File {path} found!");
            m_lastSelectedFile = path;
            return true;
        }
        else Debug.LogError($"Invalid file at {path} (Is it a .csv or is the path incorrect?)");

        return false;
    }
    private void LoadDic(string csvPath)
    {
        //Read the file
        var linesOut = File.ReadLines(csvPath);
        //save everything to the dic for ez edits
        m_loadedStats = new Dictionary<string, StatBlock>();
        foreach(var line in linesOut)
        {   
            //Split and save each line
            string[] values = line.Split(',');
            m_loadedStats.Add(values[0], new StatBlock(0, float.Parse(values[1]), float.Parse(values[2])));
        }

    }
    private VisualElement DrawDic()
    {
        //just some settings
        float generalPadding = 5;
        float nameWidth = 200;
        float numWidth = 70;

        //Create a container to display the elements
        VisualElement container = new VisualElement();
        container.style.paddingTop = generalPadding;

        //set up the saving
        //statEntryFields = new TextField[m_loadedStats.Count() * 3];

        //titles
        VisualElement titles = new VisualElement();
        titles.style.alignSelf = Align.FlexStart;
        titles.style.flexDirection = FlexDirection.Row;
        Label nameLabel = new Label("Entry Name");
        nameLabel.style.width = nameWidth;
        nameLabel.style.paddingLeft = generalPadding;
        titles.Add(nameLabel);
        Label lowLabel = new Label("Low Cap");
        lowLabel.style.width = numWidth;
        lowLabel.style.paddingLeft = generalPadding;
        titles.Add(lowLabel);
        Label highLabel = new Label("High Cap");
        highLabel.style.width = numWidth;
        highLabel.style.width = generalPadding;
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
            entryNameField.style.width = nameWidth;
            entry.Add(entryNameField);
            newFields.Add(entryNameField);

            FloatField entryFlagLowValue = new FloatField();
            entryFlagLowValue.value = dataEntry.Value.lowCap;
            entryFlagLowValue.style.width = numWidth;
            entry.Add(entryFlagLowValue);
            newFields.Add(entryFlagLowValue);

            FloatField entryFlagHighValue = new FloatField();
            entryFlagHighValue.value = dataEntry.Value.highCap;
            entryFlagHighValue.style.width = numWidth;
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
