using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


    public class TFEasyPrefabBrowser : EditorWindow
    {

        private bool canRender = true;

        struct SPagination
        {
            public int page;
            public int fromIndex;
            public int toIndex;
            public int last;
            public int totalPages;

            public bool goBack;
            public bool goNext;
            public string infos;

            public int size;
        }

        struct SMouse
        {
            public List<Rect> positions;
        }

        private List<GameObject> prefabs = new List<GameObject>();
        private string folder;

        private List<Editor> gameObjectEditors = new List<Editor>();

        private int spaceH = 2;
        private int spaceV = 20;
        Vector2 scrollPos;
        private int tab = 0;

        public enum ECOLORS
        {
            Dark = 0,
            Medium = 1,
            Light = 2,
            Green = 3,
            Blue = 4,
            Magenta = 5
        }

        public enum ESIZEMODE
        {
            ByItemSize = 0,
            ByColumnsNumber = 1
        }

        public ECOLORS op = ECOLORS.Medium;
        public ESIZEMODE mode = ESIZEMODE.ByColumnsNumber;
        public int itemWidth = 100;
        public int columns = 3;
        public string filter = "*";
        public int totalItems = 0;
        public bool showItemNumber = true;
        public bool useInteractivePreview = true;
        public bool showBorders = true;
        public bool loadModels = false;

        private SPagination pagination;
        private SMouse mouse;
        private int loadedPrefabs = 0;
        private string objectType = "t:Prefab";

        private static TFEasyPrefabBrowser window;

        [MenuItem("Window/TigerForge/Easy Prefabs Browser")]
        static void Init()
        {
            window = (TFEasyPrefabBrowser)EditorWindow.GetWindow(typeof(TFEasyPrefabBrowser));
            var content = EditorGUIUtility.IconContent("d_GridLayoutGroup Icon");
            content.text = " Easy Prefabs Browser";
            window.titleContent = content;
            window.Show();
        }

        void OnEnable()
        {
            if (window != null)
            {
                var content = EditorGUIUtility.IconContent("d_GridLayoutGroup Icon");
                content.text = " Easy Prefabs Browser";
                window.titleContent = content;
            }
        }

        void OnGUI()
        {
            GlobalInterface();

            switch (tab)
            {
                case 0:

                    filter = EditorGUILayout.TextField(GLabel("Search filter:", "SEARCH FILTER\nFilter the Prefabs list by file name. Look at the 'Help' in the 'Settings' tab for details."), filter);

                    var buttonLabel = "LOAD PREFABS";
                    if (!canRender) buttonLabel = "Waiting to load " + loadedPrefabs + " " + ((loadModels) ? "3D Models" : "Prefabs") + "...";
                    if (GUILayout.Button(buttonLabel)) GetPrefabList();

                    Rect r = EditorGUILayout.BeginHorizontal();
                    pagination.goBack = GUILayout.Button("<<");
                    EditorGUILayout.LabelField(pagination.infos, Style(Color.white, 10, TextAnchor.MiddleCenter));
                    pagination.goNext = GUILayout.Button(">>");
                    EditorGUILayout.EndHorizontal();

                    if (pagination.size < 1 || pagination.size > 60) pagination.size = 10;
                    pagination.totalPages = (int)Mathf.Ceil((float)totalItems / pagination.size);

                    if (pagination.goBack)
                    {
                        pagination.page--;
                        if (pagination.page < 0) pagination.page = 0;
                        scrollPos.y = 0;
                    }

                    if (pagination.goNext)
                    {
                        pagination.page++;
                        if (pagination.page >= pagination.totalPages) pagination.page = pagination.totalPages - 1;
                        scrollPos.y = 0;
                    }

                    if (prefabs.Count > 0 && canRender)
                    {
                        var itemSize = itemWidth;
                        var cols = Mathf.Floor((position.width - 12) / (itemSize + spaceH));
                        if (mode == ESIZEMODE.ByColumnsNumber)
                        {
                            cols = columns;
                            itemSize = (int)Mathf.Floor((position.width - 12) / cols) - spaceH;
                        }

                        var maxWidth = (itemSize + spaceH) * cols;
                        var maxHeight = ((Mathf.Min(prefabs.Count, pagination.size) / cols) * (itemSize + spaceV)) + pagination.size;
                        var startX = 10;
                        var x = startX;
                        var y = 64;

                        pagination.infos = "Page " + (pagination.page + 1) + " of " + pagination.totalPages + " (Total: " + totalItems + " Prefabs)";

                        Rect workArea = GUILayoutUtility.GetRect(10, 10000, 10, 10000);
                        scrollPos = GUI.BeginScrollView(workArea, scrollPos, new Rect(6, 60, maxWidth, maxHeight));

                        Color background = Color.black;
                        if (op == ECOLORS.Medium) background = new Color32(52, 52, 52, 100);
                        if (op == ECOLORS.Light) background = Color.grey;
                        if (op == ECOLORS.Green) background = new Color32(0, 117, 64, 100);
                        if (op == ECOLORS.Blue) background = new Color32(0, 71, 187, 100);
                        if (op == ECOLORS.Magenta) background = new Color32(255, 0, 144, 100);

                        pagination.fromIndex = pagination.page * pagination.size;
                        pagination.toIndex = pagination.fromIndex + pagination.size;
                        if (pagination.toIndex >= totalItems) pagination.toIndex = totalItems;

                        if (pagination.page != pagination.last)
                        {
                            foreach (var ed in gameObjectEditors) DestroyImmediate(ed);
                            gameObjectEditors = new List<Editor>();
                        }

                        Editor gameObjectEditor = null;
                        var counter = 0;
                        mouse.positions = new List<Rect>();
                        var gameObjectEditorsLength = gameObjectEditors.Count;

                        for (var i = pagination.fromIndex; i < pagination.toIndex; i++)
                        {
                            var gameObject = prefabs[i];

                            if (pagination.page != pagination.last)
                            {
                                gameObjectEditor = Editor.CreateEditor(gameObject);
                                gameObjectEditors.Add(gameObjectEditor);
                            }
                            else
                            {
                                if (counter >= gameObjectEditorsLength)
                                {
                                    pagination.page = 0;
                                    //EditorGUI.LabelField(new Rect(startX, 64, 200, 100), "This change of settings requires a refresh of the Prefabs list.", Style(new Color(1, 1, 1, 0.8f), 10, TextAnchor.UpperLeft));
                                    return;
                                }
                                gameObjectEditor = gameObjectEditors[counter];
                                counter++;
                            }

                            Rect rectPrefab = new Rect(x, y, itemSize, itemSize);
                            GUIStyle gStyle = new GUIStyle();
                            gStyle.normal.background = MakeTex(itemSize, itemSize, background);
                            mouse.positions.Add(new Rect(x, y, itemSize, itemSize));
                            if (useInteractivePreview) gameObjectEditor.OnInteractivePreviewGUI(rectPrefab, gStyle); else gameObjectEditor.OnPreviewGUI(rectPrefab, gStyle);

                            Rect labelBgRect = new Rect(x, y + itemSize, itemSize, spaceV - 2);
                            EditorGUI.DrawRect(labelBgRect, new Color(0, 0, 0, 0.6f));

                            Rect labelRect = new Rect(x + 2, y + itemSize, itemSize - 4, spaceV);
                            GUIContent label = new GUIContent(gameObject.name, gameObject.name);
                            EditorGUI.LabelField(labelRect, label, Style(new Color(1, 1, 1, 0.8f), 10, TextAnchor.UpperCenter));

                            if (showBorders)
                            {
                                EditorGUI.DrawRect(new Rect(rectPrefab.x, rectPrefab.y, rectPrefab.width, 1), new Color(1, 1, 1, 0.5f));
                                EditorGUI.DrawRect(new Rect(labelBgRect.x, labelBgRect.y + labelBgRect.height - 1, labelBgRect.width, 1), new Color(1, 1, 1, 0.5f));
                                EditorGUI.DrawRect(new Rect(rectPrefab.x, rectPrefab.y, 1, rectPrefab.height + labelBgRect.height), new Color(1, 1, 1, 0.5f));
                                EditorGUI.DrawRect(new Rect(rectPrefab.x + rectPrefab.width - 1, rectPrefab.y, 1, rectPrefab.height + labelBgRect.height), new Color(1, 1, 1, 0.5f));
                            }

                            if (showItemNumber)
                            {
                                EditorGUI.LabelField(new Rect(x + 2, y, itemSize, itemSize), "" + (i + 1), Style(new Color(1, 1, 1, 0.8f), 10, TextAnchor.UpperLeft));
                            }

                            x += itemSize + spaceH;

                            if (x > maxWidth)
                            {
                                x = startX;
                                y += itemSize + spaceV;
                            }

                            if (i == pagination.toIndex - 1) pagination.last = pagination.page;
                        }

                        GUILayout.BeginArea(new Rect(6, 60, maxWidth, maxHeight));
                        GUILayout.EndArea();
                        GUI.EndScrollView();

                    }

                    break;

                case 1:

                    EditorGUILayout.Separator();

                    EditorGUILayout.LabelField("LIST GENERATON", Style(Color.white, 10, TextAnchor.UpperLeft, FontStyle.Bold));

                    EditorGUILayout.Separator();

                    mode = (ESIZEMODE)EditorGUILayout.EnumPopup(GLabel("Preview Size Mode", "PREVIEW SIZE MODE\nSet how the Prefab preview size has to be calculated.\n\nBy Item Size: use a size in pixels as specified in 'Preview Size' field.\nBy Columns Number: use the full window width divided by the value specified in 'Number of Columns' field."), mode);

                    itemWidth = EditorGUILayout.IntField(GLabel("Preview Size [px]", "PREVIEW SIZE\nSpecify the width in pixels of the Prefab previews. This size is used when 'By Item Size' option is selected in the 'Preview Size Mode' menu."), itemWidth);
                    columns = EditorGUILayout.IntField(GLabel("Number of Columns", "NUMBER OF COLUMNS\nSpecify in how many columns the window width has to be divided, in order to resize the Prefab previews. This value is used when 'By Columns Number' option is selected in the 'Preview Size Mode' menu."), columns);

                    if (itemWidth < 50) itemWidth = 50;
                    if (columns < 1) columns = 1;
                    if (columns > 10) columns = 10;

                    pagination.size = EditorGUILayout.IntField(GLabel("Prefabs per Page", "PREFABS PER PAGE\nSpecify the maximum number of Prefab previews to show in the window. A pagination system is automatically activated in order to show all the available Prefabs."), pagination.size);
                    if (pagination.size < 1 || pagination.size > 60) pagination.size = 10;

                    EditorGUILayout.Separator();

                    EditorGUILayout.LabelField("LIST LOOK", Style(Color.white, 10, TextAnchor.UpperLeft, FontStyle.Bold));

                    EditorGUILayout.Separator();

                    op = (ECOLORS)EditorGUILayout.EnumPopup(GLabel("Background Color", "BACKGROUND COLOR\nChange the background color of the Prefab preview."), op);
                    showItemNumber = EditorGUILayout.Toggle(GLabel("Show Item Number", "SHOW ITEM NUMBER\nShow an index number in the left top corner of each preview."), showItemNumber);
                    showBorders = EditorGUILayout.Toggle(GLabel("Show Item Borders", "SHOW ITEM BORDERS\nShow a white border around each preview."), showBorders);

                    EditorGUILayout.Separator();

                    EditorGUILayout.LabelField("OTHER SETTINGS", Style(Color.white, 10, TextAnchor.UpperLeft, FontStyle.Bold));

                    EditorGUILayout.Separator();

                    loadModels = EditorGUILayout.Toggle(GLabel("Load Models", "LOAD MODELS\nLoad 3D Models instead of Prefabs."), loadModels);
                    useInteractivePreview = EditorGUILayout.Toggle(GLabel("Interactive Previews", "INTERACTIVE PREVIEWS\nSpecify if to use Unity OnInteractivePreviewGUI or OnPreviewGUI features. See Unity manual for details."), useInteractivePreview);

                    if (loadModels) objectType = "t:Model"; else objectType = "t:Prefab";

                    EditorGUILayout.Separator();

                    EditorGUILayout.LabelField("HELP", Style(Color.white, 10, TextAnchor.UpperLeft, FontStyle.Bold));

                    EditorGUILayout.Separator();

                    EditorGUILayout.HelpBox(
                        "• MOUSE OPERATIONS\n\nLeft click + [SHIFT]: select the prefab on the Project tab.\nLeft click + [CTRL]: insert the prefab in the current Scene.",
                        MessageType.Info);
                    EditorGUILayout.HelpBox(
                        "• SEARCH FILTER\nFilter the list by prefab file name (case insensitive).\n\nstring: exact file name.\nstring*: file name starting with string.\n*string: file name ending with string.\n*string*: file name containing the string.",
                        MessageType.Info);
                    EditorGUILayout.HelpBox(
                         "• LIMITS\nSome value limits are applied to Settings in order to have better performances.\n\nPreview size: min. 50 pixels.\nNumber of colums: min. 1, max. 10\nPrefabs per page: min. 1, max 60",
                         MessageType.Warning);
                    EditorGUILayout.HelpBox(
                        "• NOTES\n\n- Preview list is limited to 60 Prefabs due some Unity memory limits.\n- Preview interaction might be not available on items with size under 60 pixels.\n- Some Settings change might request a list refresh.",
                        MessageType.Info);

                    break;

                default:
                    break;
            }

            Event e = Event.current;
            if (e.isMouse) OnMouseClick(e);

        }


        #region " MOUSE "

        void OnMouseClick(Event e)
        {
            if (e.button == 0)
            {
                if (e.control)
                {

                    for (var i = 0; i < mouse.positions.Count; i++)
                    {
                        Vector2 mousePosition = new Vector2(e.mousePosition.x, e.mousePosition.y + scrollPos.y);
                        if (mouse.positions[i].Contains(mousePosition))
                        {
                            var index = (pagination.page * pagination.size) + i;
                            var go = prefabs[index];
                            go.transform.position = new Vector3();
                            PrefabUtility.InstantiatePrefab(go);
                        }
                    }

                }
                else if (e.shift)
                {

                    for (var i = 0; i < mouse.positions.Count; i++)
                    {
                        Vector2 mousePosition = new Vector2(e.mousePosition.x, e.mousePosition.y + scrollPos.y);
                        if (mouse.positions[i].Contains(mousePosition))
                        {
                            var index = (pagination.page * pagination.size) + i;
                            var go = prefabs[index];
                            //var path = AssetDatabase.GetAssetPath(go);
                            Selection.activeObject = go;
                        }
                    }

                }
            }
        }

        #endregion


        #region " INTERFACE "

        private void GlobalInterface()
        {
            var tabBrowser = EditorGUIUtility.IconContent("d_SceneViewOrtho");
            tabBrowser.text = "Browser";

            var tabSettings = EditorGUIUtility.IconContent("d_SettingsIcon");
            tabSettings.text = "Settings";

            GUIContent[] toolbar = new GUIContent[] { tabBrowser, tabSettings };
            tab = GUILayout.Toolbar(tab, toolbar);
        }

        void OnInspectorUpdate()
        {
            if (EditorWindow.focusedWindow == this &&
                EditorWindow.mouseOverWindow == this)
            {
                this.Repaint();
            }
        }

        #endregion


        #region " HELPERS "

        public async void GetPrefabList()
        {
            canRender = false;

            prefabs = new List<GameObject>();

            folder = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (folder == null || folder == "")
            {
                Debug.LogWarning("[EASY PREFAB BROWSER] Select a folder where to search for Prefabs.");
                return;
            }

            var guids = AssetDatabase.FindAssets(objectType, new[] { folder });
            loadedPrefabs = guids.Length;

            await System.Threading.Tasks.Task.Delay(100);

            totalItems = 0;
            pagination.page = 0;
            pagination.last = -1;

            if (filter == "" || filter == "*")
            {

                foreach (string guid in guids)
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    var obj = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
                    prefabs.Add(obj);
                    totalItems++;
                    await System.Threading.Tasks.Task.Delay(1);
                }

            }
            else
            {

                bool startsWith = filter.StartsWith("*");
                bool endsWith = filter.EndsWith("*");
                string find = filter.Replace("*", "").ToLower();

                foreach (string guid in guids)
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    var fileName = Path.GetFileNameWithoutExtension(assetPath).ToLower();
                    bool isValid = false;

                    if (startsWith && endsWith)
                    {
                        isValid = fileName.Contains(find);
                    }
                    else if (startsWith)
                    {
                        isValid = fileName.StartsWith(find);
                    }
                    else if (endsWith)
                    {
                        isValid = fileName.EndsWith(find);
                    }
                    else
                    {
                        isValid = fileName == find;
                    }

                    if (isValid)
                    {
                        var obj = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
                        prefabs.Add(obj);
                        totalItems++;
                        await System.Threading.Tasks.Task.Delay(1);
                    }

                }

            }

            canRender = true;

        }

        GUIStyle Style(Color color, int size, TextAnchor align, FontStyle fontStyle = FontStyle.Normal)
        {
            GUIStyle infoStyle = new GUIStyle();
            infoStyle.alignment = align;
            infoStyle.normal.textColor = color;
            infoStyle.fontSize = size;
            infoStyle.fontStyle = fontStyle;

            return infoStyle;
        }

        GUIContent GLabel(string text, string tooltip = "", string icon = "")
        {
            GUIContent content = new GUIContent();
            if (icon != "") content = EditorGUIUtility.IconContent(icon);

            content.text = text;
            content.tooltip = tooltip;

            return content;

        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        #endregion






    }

