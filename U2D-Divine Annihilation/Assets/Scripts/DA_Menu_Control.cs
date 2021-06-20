//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Scroll through and toggle menu objects
// Applied to: A menu parent object in a scene
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DA_Menu_Control : MonoBehaviour
{
    // Variables Scrolling
    bool horizontalScrolling;

    // Variables Sprite Scrolling
    bool scrollImages;
    bool showScrollSprite;
    public string[] testingString = new string[0];

    // Variables String Scrolling
    bool scrollStrings;
    bool showTextObjectTargets;
    bool showBaseText;
    bool showHoveredText;
}


/*


    // Variables for Custom Inspector Code
    #region Editor

    #if UNITY_EDITOR

    [CustomEditor(typeof(DA_Menu_Control))]
    [CanEditMultipleObjects]
    public class MenuControlEditor : Editor
    {
        SerializedObject serializedObj;
        DA_Menu_Control myClassScript;
        SerializedProperty myArray;

        void OnEnable()
        {
            serializedObj = new SerializedObject(target);
            myClassScript = (DA_Menu_Control)target;
            myArray = serializedObject.FindProperty("baseText2");
        }

        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();

            // Serialize after base (otherwise the drawing in the inspector will break)
            DA_Menu_Control menuControl = (DA_Menu_Control)target; // Set the current script to the target class

            serializedObj.Update();
            EditorGUILayout.PropertyField(myArray);
            serializedObj.ApplyModifiedProperties();

            if (menuControl.currentMenu == ItemType.scrollSprites)
            {
                DrawScrollImages(menuControl);
                DrawImagesList(menuControl);
            }

            if (menuControl.currentMenu == ItemType.scrollStrings)
            {

                //DrawScrollStrings(menuControl);
                //DrawTextObjectTargets(menuControl);
                //DrawBaseText(menuControl);
                //DrawHoveredText(menuControl);
            }

            if (menuControl.currentMenu == ItemType.menuControl)
            {

            }
            //EditorApplication.MarkSceneDirty();
        }

        // Scroll Sprites
        static void DrawScrollImages(DA_Menu_Control menuControl)
        {
            EditorGUILayout.Space();
            menuControl.scrollImages = EditorGUILayout.Toggle("Scroll Sprites", menuControl.scrollImages);
            EditorGUILayout.Space();
            menuControl.horizontalScrolling = EditorGUILayout.Toggle("Horizontal Scrolling", menuControl.horizontalScrolling);
        }

        static void DrawImagesList(DA_Menu_Control menuControl)
        {
            menuControl.showScrollSprite = EditorGUILayout.Foldout(menuControl.showScrollSprite, "Scroll Sprites", true); // ######CHANGE ME######
            if (menuControl.showScrollSprite) // ######CHANGE ME######
            {
                EditorGUI.indentLevel++;
                List<GameObject> list = menuControl.scrollSprite; // ######CHANGE ME######
                int listSize = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count)); // Size of List
                                                                                           // Correct the size of list
                while (listSize > list.Count)
                {
                    list.Add(null);
                }
                while (listSize < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }
                // Serialize list
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(GameObject), true) as GameObject;
                }
                EditorGUI.indentLevel--;
            }
        }


        // Scroll Strings
        static void DrawScrollStrings(DA_Menu_Control menuControl)
        {
            EditorGUILayout.Space();
            menuControl.scrollImages = EditorGUILayout.Toggle("Scroll Strings", menuControl.scrollImages);
            EditorGUILayout.Space();
            menuControl.horizontalScrolling = EditorGUILayout.Toggle("Horizontal Scrolling", menuControl.horizontalScrolling);
        }

        static void DrawTextObjectTargets(DA_Menu_Control menuControl)
        {
            menuControl.showScrollSprite = EditorGUILayout.Foldout(menuControl.showScrollSprite, "Text Object Targets", true); // ######CHANGE ME######
            if (menuControl.showScrollSprite) // ######CHANGE ME######
            {
                EditorGUI.indentLevel++;
                List<Text> list = menuControl.textObjectTargets; // ######CHANGE ME######
                int listSize = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count)); // Size of List
                                                                                           // Correct the size of list
                while (listSize > list.Count)
                {
                    list.Add(null);
                }
                while (listSize < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }
                // Serialize list
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(Text), true) as Text;
                }
                EditorGUI.indentLevel--;
            }
        }

        static void DrawBaseText(DA_Menu_Control menuControl)
        {
            menuControl.showBaseText = EditorGUILayout.Foldout(menuControl.showBaseText, "Base Text", true); // ######CHANGE ME######
            if (menuControl.showBaseText) // ######CHANGE ME######
            {
                EditorGUI.indentLevel++;
                List<string> list = menuControl.baseText; // ######CHANGE ME######
                int listSize = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count)); // Size of List
                                                                                           // Correct the size of list
                while (listSize > list.Count)
                {
                    list.Add(null);
                }
                while (listSize < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }
                // Serialize list
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = EditorGUILayout.TextField(list[i]) as string;
                }
                EditorGUI.indentLevel--;
            }
        }

        static void DrawHoveredText(DA_Menu_Control menuControl)
        {
            menuControl.showHoveredText = EditorGUILayout.Foldout(menuControl.showHoveredText, "Hovered Text", true); // ######CHANGE ME######
            if (menuControl.showHoveredText) // ######CHANGE ME######
            {
                EditorGUI.indentLevel++;
                List<Text> list = menuControl.hoveredText; // ######CHANGE ME######
                int listSize = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count)); // Size of List
                                                                                           // Correct the size of list
                while (listSize > list.Count)
                {
                    list.Add(null);
                }
                while (listSize < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }
                // Serialize list
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(Text), true) as Text;
                }
                EditorGUI.indentLevel--;
            }
        }

    }

    #endif

    #endregion

}


/*
    // Sprites
    [Header ("Scrolling With Sprites - (Don't touch if left unchecked)")]
    public bool scrollImages;

    [ConditionalHide("scrollImages", true)]
    public Sprite[] imageFrames;
    [ConditionalHide("scrollImages", true)]
    public bool horizontalScrollingImages;

    // Strings
    [Header("Scrolling With Strings - (Don't touch if left unchecked)")]
    public bool scrollStrings;

    [ConditionalHide("scrollStrings", true)]
    public Text[] UIElements;
    [ConditionalHide("scrollStrings", true)]
    public string[] defaultText;
    [ConditionalHide("scrollStrings", true)]
    public string[] hoverText;
    [ConditionalHide("scrollStrings", true)]
    public bool horizontalScrollingString;

    // Controlling
    [Header ("Menu Control - (Don't touch if left unchecked)")]
    public bool control;

    [ConditionalHide("control", true)]
    public bool wrapAround;

    [ConditionalHide("control", true)]
    public bool canGoBack;

    [ConditionalHide("canGoBack", true)]
    public GameObject activeOnBack;
    [ConditionalHide("control", true)]
    public GameObject selfMenuObject;*/