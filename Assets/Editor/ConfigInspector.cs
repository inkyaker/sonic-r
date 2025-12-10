using Luau;
using Unity.VisualScripting;
using UnityEngine;

[CustomAirshipEditor("Config")]
public class ConfigEditor : AirshipEditor
{
    private int Tab = 0;
    public override void OnInspectorGUI()
    {
        Tab = AirshipEditorGUI.BeginTabs(Tab, new[]
        {
            new GUIContent("Animation"),
            new GUIContent("Control"),
            new GUIContent("UI"),
            new GUIContent("Framework"),
            new GUIContent("CharacterInfo")
        });

        switch (Tab)
        {
            case 0:
                DrawAnimationTab();
                break;
            case 1:
                DrawControlTab();
                break;
            case 2:
                DrawUITab();
                break;
            case 3:
                DrawFrameworkTab();
                break;
            case 4:
                DrawCharacterInfoTab();
                break;
        }

        AirshipEditorGUI.EndTabs();
    }

    void DrawAnimationTab()
    {
        AirshipEditorGUI.BeginGroup(new GUIContent("Animation"));
        PropertyFields("RigAnimationTilt", "HeadTilt", "EyeTilt");
        AirshipEditorGUI.EndGroup();

        AirshipEditorGUI.BeginGroup(new GUIContent("JumpBall"));
        PropertyFields("JumpBallStretchCurve", "JumpBallRotationSpeed");
        AirshipEditorGUI.EndGroup();

        AirshipEditorGUI.BeginGroup(new GUIContent("SpindashBall"));
        PropertyFields("SpindashBallRotationSpeed");
        AirshipEditorGUI.EndGroup();
    }

    void DrawUITab()
    {
        PropertyFields("ReticleMaxDistance", "ReticleDistanceCurve", "ReticleTimeMax", "ReticleRotationSpeed");
    }

    void DrawControlTab()
    {
        PropertyFields("CameraSensitivityCurve");
    }

    void DrawFrameworkTab()
    {
        PropertyFields("GameSpeed", "Tickrate", "CollisionLayer", "ObjectLayer", "RailLayer");
    }

    void DrawCharacterInfoTab()
    {
        PropertyField("Character");
        AirshipEditorGUI.BeginGroup(new GUIContent("Character Properties"));
        
        // to fix the deprecated warning. i think this is worse
        var Character = serializedObject.targetObject.GameObject().GetAirshipComponent(AirshipType.GetType("Character"));
        if (Character)
        {
            CharacterEditor Editor = (CharacterEditor)AirshipCustomEditors.GetEditor(Character);
            Editor.DrawContained();
        }

        AirshipEditorGUI.EndGroup();
    }
}