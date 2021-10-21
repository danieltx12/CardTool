using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;




public class CardDesigner : EditorWindow
{

    GUISkin skin;

    Texture2D headerSectionTexture;
    Texture2D boostSectionTexture;
    Texture2D attackSectionTexture;
    Texture2D reactSectionTexture;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect headerSection;
    Rect boostSection;
    Rect attackSection;
    Rect reactSection;

    static AttackData attackData;
    static ReactData reactData;
    static BoostData boostData;

    public static AttackData AttackInfo { get { return attackData; } }
    public static BoostData BoostInfo { get { return boostData; } }
    public static ReactData ReactInfo { get { return reactData; } }


    [MenuItem("Window/Card Designer")]
    public static void OpenWindow()
    {
        CardDesigner window = (CardDesigner)GetWindow(typeof(CardDesigner));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    void OnEnable()
    {
        InitTextures();
        InitData();
        skin = Resources.Load<GUISkin>("GuiStyles/CardDesignerSkin");

    }

    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawBoostSettings();
        DrawReactSettings();
        DrawAttackSettings();

    }

    void DrawLayouts()
    {
        float test = Screen.width / 3f;
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        boostSection.x = 0;
        boostSection.y = 50;
        boostSection.width = test;
        boostSection.height = Screen.width - 50;

        attackSection.x = test;
        attackSection.y = 50;
        attackSection.width = test;
        attackSection.height = Screen.width - 50;

        reactSection.x = (test * 2);
        reactSection.y = 50;
        reactSection.width = test;
        reactSection.height = Screen.width - 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(attackSection, attackSectionTexture);
        GUI.DrawTexture(reactSection, reactSectionTexture);
        GUI.DrawTexture(boostSection, boostSectionTexture);
    }

    void DrawHeader()
    {
       
            GUILayout.BeginArea(headerSection);

            GUILayout.Label("Card Designer", skin.GetStyle("Header"));

            GUILayout.EndArea();
    }

    public static void InitData()
    {
        attackData = (AttackData)ScriptableObject.CreateInstance(typeof(AttackData));
        boostData = (BoostData)ScriptableObject.CreateInstance(typeof(BoostData));
        reactData = (ReactData)ScriptableObject.CreateInstance(typeof(ReactData));
    }

    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        attackSectionTexture = Resources.Load<Texture2D>("icons/editor_attack");
        boostSectionTexture = Resources.Load<Texture2D>("icons/editor_boost");
        reactSectionTexture = Resources.Load<Texture2D>("icons/editor_react");

    }

    void DrawBoostSettings()
    {
        GUILayout.BeginArea(boostSection);

        GUILayout.Label("Boost", skin.GetStyle("BoostHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Stat Increase", skin.GetStyle("BoostValues"));
        boostData.statIncrease = (StatIncrease)EditorGUILayout.EnumPopup(boostData.statIncrease);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Boost Percent", skin.GetStyle("BoostValues"));
        boostData.boostPercent = EditorGUILayout.IntSlider(boostData.boostPercent, 1, 100);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.BOOST);
            this.Close();
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    void DrawReactSettings()
    {
        GUILayout.BeginArea(reactSection);

        GUILayout.Label("React", skin.GetStyle("ReactHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("React To: ", skin.GetStyle("ReactValues"));
        reactData.reactType = (ReactType)EditorGUILayout.EnumPopup(reactData.reactType);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.REACT);
            this.Close();
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    void DrawAttackSettings()
    {
        GUILayout.BeginArea(attackSection);

        GUILayout.Label("Attack", skin.GetStyle("AttackHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage Type", skin.GetStyle("AttackValues"));
        attackData.damageType = (DamageType)EditorGUILayout.EnumPopup(attackData.damageType);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power", skin.GetStyle("AttackValues"));
        attackData.power = EditorGUILayout.Slider(attackData.power, 1, 100);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health", skin.GetStyle("AttackValues"));
        attackData.maxHealth = EditorGUILayout.IntField(attackData.maxHealth);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ATTACK);
            this.Close();
            
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
    }


}
public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        ATTACK,
        REACT,
        BOOST
    }
    static SettingsType dataSetting;
    static GeneralSettings window;

    public static void OpenWindow(SettingsType setting)
    {
        dataSetting = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    void OnGUI()
    {
        switch (dataSetting)
        {
            case SettingsType.REACT:
                DrawSettings((CardData)CardDesigner.ReactInfo);
                break;
            case SettingsType.ATTACK:
                DrawSettings((CardData)CardDesigner.AttackInfo);
                break;
            case SettingsType.BOOST:
                DrawSettings((CardData)CardDesigner.BoostInfo);
                break;
        }
    }

    void DrawSettings(CardData cardData)
    {

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        cardData.prefab = (GameObject)EditorGUILayout.ObjectField(cardData.prefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Front Image");
        cardData.frontImage = (Texture2D)EditorGUILayout.ObjectField(cardData.frontImage, typeof(Texture2D), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Back Image");
        cardData.backImage = (Texture2D)EditorGUILayout.ObjectField(cardData.backImage, typeof(Texture2D), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Description");
        cardData.description = EditorGUILayout.TextField(cardData.description);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        cardData.name = EditorGUILayout.TextField(cardData.name);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mana Cost");
        cardData.manaCost = EditorGUILayout.IntField(cardData.manaCost);
        EditorGUILayout.EndHorizontal();
        
        if (cardData.frontImage == null)
        {
            EditorGUILayout.HelpBox("This card needs a [Front Image] before it can be created.", MessageType.Warning);
        }
        else if (cardData.backImage == null)
        {
            EditorGUILayout.HelpBox("This card needs a [Back Image] before it can be created.", MessageType.Warning);
        }
        else if (cardData.name == null || cardData.name.Length < 1)
        {
            EditorGUILayout.HelpBox("This card needs a [Name] before it can be created.", MessageType.Warning);
        }
        else if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
        {
            this.Close();
            SaveCardData();
            

        }
        


    }


    void SaveCardData()
    {
        string prefabPath;
        string newPrefabPath = "Assets/Prefabs/Cards";
        string dataPath = "Assets/Resources/CardData/Data/";

        switch (dataSetting)
        {
            case SettingsType.REACT:

                dataPath += "react/" + CardDesigner.ReactInfo.name + ".asset";
                AssetDatabase.CreateAsset(CardDesigner.ReactInfo, dataPath);

                newPrefabPath += "react/" + CardDesigner.ReactInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(CardDesigner.ReactInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject reactPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!reactPrefab.GetComponent<React>())
                    reactPrefab.AddComponent(typeof(React));
                reactPrefab.GetComponent<React>().reactData = CardDesigner.ReactInfo;
                

                break;

            case SettingsType.ATTACK:
                dataPath += "attack/" + CardDesigner.AttackInfo.name + ".asset";
                AssetDatabase.CreateAsset(CardDesigner.AttackInfo, dataPath);

                newPrefabPath += "attack/" + CardDesigner.AttackInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(CardDesigner.AttackInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject attackPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!attackPrefab.GetComponent<Attack>())
                    attackPrefab.AddComponent(typeof(Attack));
                attackPrefab.GetComponent<Attack>().attackData = CardDesigner.AttackInfo;
                
                break;

            case SettingsType.BOOST:

                dataPath += "boost/" + CardDesigner.BoostInfo.name + ".asset";
                AssetDatabase.CreateAsset(CardDesigner.BoostInfo, dataPath);

                newPrefabPath += "boost/" + CardDesigner.BoostInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(CardDesigner.BoostInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject boostPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!boostPrefab.GetComponent<Boost>())
                    boostPrefab.AddComponent(typeof(Boost));
                boostPrefab.GetComponent<Boost>().boostData = CardDesigner.BoostInfo;
                

                break;
        }
    }
}
