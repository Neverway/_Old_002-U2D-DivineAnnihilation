using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Xml.Serialization;
using UnityEngine.Events;

namespace TigerForge
{
    /// <summary>
    /// Easy File Save v.1.1
    /// </summary>
    public class EasyFileSave
    {

        #region " VARIABLES & PROPERTIES "

        private Dictionary<string, object> storage = new Dictionary<string, object>();

        /// <summary>
        /// The error information.
        /// </summary>
        public string Error = "";

        /// <summary>
        /// Disable the warning messages shown in the Console. 
        /// </summary>
        public bool suppressWarning = true;

        private readonly string fileName = "";

        public struct UnityTransform
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 localScale;
            public Vector3 localPosition;
            public Quaternion localRotation;
            public Vector3 lossyScale;
            public Vector3 eulerAngles;
            public Vector3 localEulerAngles;
        }

        private EasyFileSaveExtension customs = new EasyFileSaveExtension();

        public struct CustomData
        {
            /// <summary>
            /// The raw object data.
            /// </summary>
            public object data;

            /// <summary>
            /// Cast the object into integer value.
            /// </summary>
            /// <returns></returns>
            public int ToInt()
            {
                try
                {
                    return (int)data;
                }
                catch (System.Exception)
                {
                    return 0;
                }
            }

            /// <summary>
            /// Cast the object into float value.
            /// </summary>
            /// <returns></returns>
            public float ToFloat()
            {
                try
                {
                    return (float)data;
                }
                catch (System.Exception)
                {
                    return 0f;
                }
            }

            /// <summary>
            /// Cast the object into byrte value.
            /// </summary>
            /// <returns></returns>
            public byte ToByte()
            {
                try
                {
                    return (byte)data;
                }
                catch (System.Exception)
                {
                    return 0;
                }
            }

            /// <summary>
            /// Cast the object into a string.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                try
                {
                    return (string)data;
                }
                catch (System.Exception)
                {
                    return "";
                }
            }

            /// <summary>
            /// Cast the object into a boolean value.
            /// </summary>
            /// <returns></returns>
            public bool ToBool()
            {
                try
                {
                    return (bool)data;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

        }

        #endregion


        #region " CONSTRUCTOR "

        /// <summary>
        /// Initialize a new instance with the given fileName or with a default file name if it's not specified.
        /// </summary>
        /// <param name="fileName"></param>
        public EasyFileSave(string fileName = "")
        {
            if (fileName == "") fileName = "gamedata";

            storage = new Dictionary<string, object>();
            this.fileName = Application.persistentDataPath + "/" + fileName + ".dat";
            Error = "";
            Register(fileName);

            customs.Start();
        }

        #endregion
        

        #region " SAVE "

        /// <summary>
        /// Save the 'system internal storage' data into the file. Return TRUE when done without errors.
        /// </summary>
        public bool Save()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream saveFile = File.Create(fileName);
                bf.Serialize(saveFile, storage);
                saveFile.Close();
                Dispose();
                return true;
            }
            catch (System.Exception e)
            {
                Error = "[Easy File Save] This system exeption has been thrown during saving: " + e.Message;
                return false;
            }
            
        }

        /// <summary>
        /// Append the 'system internal storage' data at the end of the current file content. By default, existing keys will be overwritten with new values. If 'overwrite' parameter is set to FALSE, existing keys will be ignored.
        /// </summary>
        public bool Append(bool overwrite = true)
        {
            try
            {
                Dictionary<string, object> fileStorage = new Dictionary<string, object>();

                if (FileExists())
                {
                    BinaryFormatter bf2 = new BinaryFormatter();
                    FileStream openFile = File.Open(fileName, FileMode.Open);
                    fileStorage = (Dictionary<string, object>)bf2.Deserialize(openFile);
                    openFile.Close();

                    foreach (KeyValuePair<string, object> item in storage)
                    {
                        if (fileStorage.ContainsKey(item.Key))
                        {
                            if (overwrite) fileStorage[item.Key] = item.Value;
                        }
                        else
                        {
                            fileStorage.Add(item.Key, item.Value);
                        }
                    }
                }
                else
                {
                    fileStorage = storage;
                }

                BinaryFormatter bf = new BinaryFormatter();
                FileStream saveFile = File.Create(fileName);
                bf.Serialize(saveFile, fileStorage);
                saveFile.Close();
                Dispose();
                return true;
            }
            catch (System.Exception e)
            {
                Error = "[Easy File Save] This system exeption has been thrown during append data: " + e.Message;
                return false;
            }

            
        }

        /// <summary>
        /// Add a value, with the given unique key, into the 'system internal storage'.
        /// <br/>By default, if the given key already exists into the 'internal storage', the existing key value is overwritten by the new value.
        /// <br/>Set the optional 'ignoreExistingKey' parameter to true so as to prevent the reuse of an existing key. In this case, the original value won't be overwritten and a non-blocking warning will be thrown.
        /// </summary>
        public void Add(string key, object value, bool ignoreExistingKey = false)
        {
            if (KeyExists(key))
            {
                if (ignoreExistingKey)
                {
                    Warning("[Easy File Save] Trying to reuse the key '" + key + "' to put a value into the storage!");
                }
                else
                {
                    value = ConvertUnityTypes(value);
                    storage[key] = value;
                }
            }
            else
            {
                value = ConvertUnityTypes(value);
                storage.Add(key, value);
            }
        }

        

        #endregion


        #region " LOAD "

        /// <summary>
        /// Load the file data into the 'system internal storage' and return TRUE if the loading has been completed. Return FALSE if something has gone wrong (use Error property to get error informations).
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            if (!FileExists())
            {
                Error = "[Easy File Save] The file " + fileName + " doesn't exist.";
                return false;
            }

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream saveFile = File.Open(fileName, FileMode.Open);

                storage = (Dictionary<string, object>)bf.Deserialize(saveFile);

                saveFile.Close();
                return true;
            }
            catch (System.Exception e)
            {
                Error = "[Easy File Save] This system exeption has been thrown during loading: " + e.Message;
                return false;
            }

        }

        #endregion


        #region " TOOLS "

        /// <summary>
        /// Return TRUE if the file exists.
        /// </summary>
        /// <returns></returns>
        public bool FileExists()
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// Delete this file (if it exists).
        /// </summary>
        public void Delete()
        {
            if (FileExists())
            {
                File.Delete(fileName);
                Dispose();
            }
        }

        /// <summary>
        /// Return TRUE if the 'system internal storage' contains the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return storage.ContainsKey(key);
        }

        /// <summary>
        /// Return the system internal storage.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetStorage()
        {
            return storage;
        }

        /// <summary>
        /// Return the current file name with path.
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return fileName;
        }

        /// <summary>
        /// Delete the 'system internal storage' so to free this part of memory. This method is automatically called after saving. It should be manually called after loading data.
        /// </summary>
        public void Dispose()
        {
            storage = new Dictionary<string, object>();
            Error = "";
        }

        private void Warning(string message)
        {
            if (!suppressWarning) Debug.LogWarning(message);
        }

        /// <summary>
        /// Perform a test of data saving and loading, so as to verifiy if the provided data is correctly managed. Return true if the test is passed. The test results are shown on Console panel.
        /// </summary>
        /// <returns></returns>
        public bool TestDataSaveLoad()
        {
            Debug.Log("==== [Easy File Save] ==== TESTING SAVE AND LOAD DATA ==============================================\n");

            if (Save())
            {
                Debug.Log("[Easy File Save] >> TEST #1 PASSED: data has been saved!\n");

                if (Load())
                {
                    Debug.Log("[Easy File Save] >> TEST #2 PASSED: data has been loaded!\n");
                    Debug.Log("====================================================================================\n");
                    return true;
                }
                else
                {
                    Debug.Log("[Easy File Save] >> TEST #2 NOT PASSED: there is a problem loading data!\n");
                    Debug.Log(Error);
                }

            }
            else
            {
                Debug.Log("[Easy File Save] >> TEST #1 NOT PASSED: there is a problem saving data!\n");
                Debug.Log(Error);
            }

            Debug.Log("====================================================================================\n");

            return false;

        }

        #endregion


        #region " GETTERS (DEFAULT TYPES) "

        /// <summary>
        /// Return the object data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key, object defaultValue = null)
        {
            try
            {
                if (storage.ContainsKey(key)) return storage[key]; else return defaultValue;
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetData error using key: " + key);
                return defaultValue;
            }
        }

        /// <summary>
        /// Return the integer data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetInt(string key, int defaultValue = 0)
        {
            try
            {
                if (storage.ContainsKey(key)) return (int)storage[key]; else return defaultValue;
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetInt error using key: " + key);
                return defaultValue;
            }
        }

        /// <summary>
        /// Return the boolean data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetBool(string key, bool defaultValue = false)
        {
            try
            {
                if (storage.ContainsKey(key)) return (bool)storage[key]; else return defaultValue;
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetBool error using key: " + key);
                return defaultValue;
            }
        }

        /// <summary>
        /// Return the float data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float GetFloat(string key, float defaultValue = 0f)
        {
            try
            {
                if (storage.ContainsKey(key)) return (float)storage[key]; else return defaultValue;
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetFloat error using key: " + key);
                return defaultValue;
            }
        }

        /// <summary>
        /// Return the string data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key, string defaultValue = "")
        {
            try
            {
                if (storage.ContainsKey(key)) return (string)storage[key]; else return defaultValue;
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetString error using key: " + key);
                return defaultValue;
            }
        }

        /// <summary>
        /// Return the byte data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte GetByte(string key, byte defaultValue = 0)
        {
            try
            {
                if (storage.ContainsKey(key)) return (byte)storage[key]; else return defaultValue;
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetByte error using key: " + key);
                return defaultValue;
            }
        }

        #endregion


        #region " SPECIAL TYPES (SETTERS & GETTERS) "

        /// <summary>
        /// Serialize an object and add it, with the given unique key, into the 'system internal storage'.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void AddSerialized(string key, object data, bool ignoreExistingKey = false)
        {
            var xml = Serialize(data);
            Add(key, xml, ignoreExistingKey);
        }

        /// <summary>
        /// Return the object data, for the given key, deserialized with the given type (or null if nothing found).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetDeserialized(string key, System.Type type)
        {
            try
            {
                var obj = GetData(key);
                if (obj != null) return Deserialize(obj, type); else return null;
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetDeserializedObject error using key: " + key);
                return null;
            }
        }

        /// <summary>
        /// Add a new custom value, with the given unique key, into the 'system internal storage'.
        /// </summary>
        public void AddCustom(string key, object data, string extensionName, bool ignoreExistingKey = false)
        {
            if (!customs.extensions.ContainsKey(extensionName))
            {
                Debug.LogWarning("[Easy File Save] AddCustom: an extension with name '" + extensionName + "doesn't exist.");
                return;
            }

            UnityAction myExtension = customs.extensions[extensionName];
            customs.data[extensionName] = data;
            myExtension.Invoke();

            List<object> dataToSave = customs.pars[extensionName];
            Add(key, dataToSave, ignoreExistingKey);
        }

        /// <summary>
        /// Return a dictionary of custom values.
        /// </summary>
        public Dictionary<string, CustomData> GetCustom(string key, string extensionName)
        {
            try
            {
                if (storage.ContainsKey(key)) {

                    if (!customs.mapping.ContainsKey(extensionName))
                    {
                        Debug.LogWarning("[Easy File Save] GetCustom: an extension with name '" + extensionName + "doesn't exist.");
                        return null;
                    }

                    List<object> dataToLoad = (List<object>)storage[key];
                    List<string> mapping = customs.mapping[extensionName];

                    if (dataToLoad.Count != mapping.Count)
                    {
                        Debug.LogWarning("[Easy File Save] GetCustom: check your extension! Something gone wrong.");
                        return null;
                    }

                    Dictionary<string, CustomData> customData = new Dictionary<string, CustomData>();
                    for (var i = 0; i < mapping.Count; i++)
                    {
                        customData.Add(mapping[i], new CustomData { data = dataToLoad[i] });
                    }

                    return customData;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetCustom error using key: " + key);
            }
            return null;
        }

        /// <summary>
        /// Convert an object in bytes and add it, with the given unique key, into the 'system internal storage'.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void AddBinary(string key, object data, bool ignoreExistingKey = false)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            var arrayData = stream.ToArray();

            Add(key, arrayData, ignoreExistingKey);
        }

        /// <summary>
        /// Return the object data for the given key. The object must be properly converted (cast) to the original data structure.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetBinary(string key)
        {
            try
            {
                if (storage.ContainsKey(key))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream stream = new MemoryStream((byte[])storage[key]);
                    var obj = formatter.Deserialize(stream);
                    return obj;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetBinary error using key: " + key);
            }
            return null;
        }

        #endregion


        #region  " UNITY TYPES (SETTERS & GETTERS) "

        private object ConvertUnityTypes(object value)
        {
            string type = value.GetType().ToString();
            if (!type.StartsWith("UnityEngine")) return value;

            List<float> converted = new List<float>();

            switch (type)
            {
                case "UnityEngine.Vector2":
                    Vector2 v2Data = (Vector2)value;
                    converted.Add(v2Data.x);
                    converted.Add(v2Data.y);

                    break;

                case "UnityEngine.Vector3":
                    Vector3 v3Data = (Vector3)value;
                    converted.Add(v3Data.x);
                    converted.Add(v3Data.y);
                    converted.Add(v3Data.z);

                    break;

                case "UnityEngine.Vector4":
                    Vector4 v4Data = (Vector4)value;
                    converted.Add(v4Data.x);
                    converted.Add(v4Data.y);
                    converted.Add(v4Data.z);
                    converted.Add(v4Data.w);

                    break;

                case "UnityEngine.Quaternion":
                    Quaternion qData = (Quaternion)value;
                    converted.Add(qData.x);
                    converted.Add(qData.y);
                    converted.Add(qData.z);
                    converted.Add(qData.w);

                    break;

                case "UnityEngine.Transform":
                    Transform trData = (Transform)value;
                    converted.Add(trData.position.x);
                    converted.Add(trData.position.y);
                    converted.Add(trData.position.z);
                    converted.Add(trData.localPosition.x);
                    converted.Add(trData.localPosition.y);
                    converted.Add(trData.localPosition.z);
                    converted.Add(trData.localScale.x);
                    converted.Add(trData.localScale.y);
                    converted.Add(trData.localScale.z);
                    converted.Add(trData.lossyScale.x);
                    converted.Add(trData.lossyScale.y);
                    converted.Add(trData.lossyScale.z);
                    converted.Add(trData.rotation.x);
                    converted.Add(trData.rotation.y);
                    converted.Add(trData.rotation.z);
                    converted.Add(trData.rotation.w);
                    converted.Add(trData.localRotation.x);
                    converted.Add(trData.localRotation.y);
                    converted.Add(trData.localRotation.z);
                    converted.Add(trData.localRotation.w);
                    converted.Add(trData.eulerAngles.x);
                    converted.Add(trData.eulerAngles.y);
                    converted.Add(trData.eulerAngles.z);
                    converted.Add(trData.localEulerAngles.x);
                    converted.Add(trData.localEulerAngles.y);
                    converted.Add(trData.localEulerAngles.z);

                    break;

                case "UnityEngine.Color":
                    Color clData = (Color)value;
                    converted.Add(clData.r);
                    converted.Add(clData.g);
                    converted.Add(clData.b);
                    converted.Add(clData.a);

                    break;

                case "UnityEngine.Color32":
                    Color32 cl32Data = (Color32)value;
                    converted.Add(cl32Data.r);
                    converted.Add(cl32Data.g);
                    converted.Add(cl32Data.b);
                    converted.Add(cl32Data.a);

                    break;

                case "UnityEngine.Rect":
                    Rect reData = (Rect)value;
                    converted.Add(reData.x);
                    converted.Add(reData.y);
                    converted.Add(reData.width);
                    converted.Add(reData.height);

                    converted.Add(reData.center.x);
                    converted.Add(reData.center.y);
                    converted.Add(reData.max.x);
                    converted.Add(reData.max.y);
                    converted.Add(reData.min.x);
                    converted.Add(reData.min.y);
                    converted.Add(reData.position.x);
                    converted.Add(reData.position.y);
                    converted.Add(reData.size.x);
                    converted.Add(reData.size.y);
                    converted.Add(reData.xMax);
                    converted.Add(reData.xMin);
                    converted.Add(reData.yMax);
                    converted.Add(reData.yMin);
                    break;

                default:
                    break;
            }

            return converted;
        }

        /// <summary>
        /// Return the Vector2 data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Vector2 GetUnityVector2(string key, Vector2 defaultValue = new Vector2())
        {
            try
            {
                if (storage.ContainsKey(key))
                {
                    List<float> dataList = (List<float>)storage[key];
                    Vector2 newData = new Vector2(dataList[0], dataList[1]);
                    return newData;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetUnityVector2 error using key: " + key);
            }
            return defaultValue;
        }

        /// <summary>
        /// Return the Vector3 data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        public Vector3 GetUnityVector3(string key, Vector3 defaultValue = new Vector3())
        {
            try
            {
                if (storage.ContainsKey(key))
                {
                    List<float> dataList = (List<float>)storage[key];
                    Vector3 newData = new Vector3(dataList[0], dataList[1], dataList[2]);
                    return newData;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetUnityVector3 error using key: " + key);
            }
            return defaultValue;
        }

        /// <summary>
        /// Return the Vector4 data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        public Vector4 GetUnityVector4(string key, Vector4 defaultValue = new Vector4())
        {
            try
            {
                if (storage.ContainsKey(key))
                {
                    List<float> dataList = (List<float>)storage[key];
                    Vector4 newData = new Vector4(dataList[0], dataList[1], dataList[2], dataList[3]);
                    return newData;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetUnityVector4 error using key: " + key);
            }
            return defaultValue;
        }

        /// <summary>
        /// Return the Quaternion data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        public Quaternion GetUnityQuaternion(string key, Quaternion defaultValue = new Quaternion())
        {
            try
            {
                if (storage.ContainsKey(key))
                {
                    List<float> dataList = (List<float>)storage[key];
                    Quaternion newData = new Quaternion(dataList[0], dataList[1], dataList[2], dataList[3]);
                    return newData;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetUnityQuaternion error using key: " + key);
            }
            return defaultValue;
        }

        /// <summary>
        /// Return the Quaternion data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        public UnityTransform GetUnityTransform(string key, UnityTransform defaultValue = new UnityTransform())
        {
            var tr = new UnityTransform();

            try
            {
                if (storage.ContainsKey(key))
                {
                    List<float> dataList = (List<float>)storage[key];
                    tr.position.x = dataList[0];
                    tr.position.y = dataList[1];
                    tr.position.z = dataList[2];
                    tr.localPosition.x = dataList[3];
                    tr.localPosition.y = dataList[4];
                    tr.localPosition.z = dataList[5];
                    tr.localScale.x = dataList[6];
                    tr.localScale.y = dataList[7];
                    tr.localScale.z = dataList[8];
                    tr.lossyScale.x = dataList[9];
                    tr.lossyScale.y = dataList[10];
                    tr.lossyScale.z = dataList[11];
                    tr.rotation.x = dataList[12];
                    tr.rotation.y = dataList[13];
                    tr.rotation.z = dataList[14];
                    tr.rotation.w = dataList[15];
                    tr.localRotation.x = dataList[16];
                    tr.localRotation.y = dataList[17];
                    tr.localRotation.z = dataList[18];
                    tr.localRotation.w = dataList[19];
                    tr.eulerAngles.x = dataList[20];
                    tr.eulerAngles.y = dataList[21];
                    tr.eulerAngles.z = dataList[22];
                    tr.localEulerAngles.x = dataList[23];
                    tr.localEulerAngles.y = dataList[24];
                    tr.localEulerAngles.z = dataList[25];
                    return tr;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetUnityTransform error using key: " + key);
            }
            return defaultValue;
        }

        /// <summary>
        /// Return the Color data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        public Color GetUnityColor(string key, Color defaultValue = new Color())
        {
            try
            {
                if (storage.ContainsKey(key))
                {
                    List<float> dataList = (List<float>)storage[key];
                    Color newData = new Color(dataList[0], dataList[1], dataList[2], dataList[3]);
                    return newData;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetUnityColor error using key: " + key);
            }
            return defaultValue;
        }

        /// <summary>
        /// Return the Color32 data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        public Color32 GetUnityColor32(string key, Color32 defaultValue = new Color32())
        {
            try
            {
                if (storage.ContainsKey(key))
                {
                    List<float> dataList = (List<float>)storage[key];
                    Color32 newData = new Color32((byte)dataList[0], (byte)dataList[1], (byte)dataList[2], (byte)dataList[3]);
                    return newData;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetUnityColor32 error using key: " + key);
            }
            return defaultValue;
        }

        /// <summary>
        /// Return the Rect data for the given key (or the defined defaultValue if nothing found).
        /// </summary>
        public Rect GetUnityRect(string key, Rect defaultValue = new Rect())
        {
            try
            {
                if (storage.ContainsKey(key))
                {
                    List<float> dataList = (List<float>)storage[key];
                    Rect newData = new Rect(dataList[0], dataList[1], dataList[2], dataList[3]);

                    if (dataList.Count == 18)
                    {
                        newData.center = new Vector2(dataList[4], dataList[5]);
                        newData.max = new Vector2(dataList[6], dataList[7]);
                        newData.min = new Vector2(dataList[8], dataList[9]);
                        newData.position = new Vector2(dataList[10], dataList[11]);
                        newData.size = new Vector2(dataList[12], dataList[13]);
                        newData.xMax = dataList[14];
                        newData.xMin = dataList[15];
                        newData.yMax = dataList[16];
                        newData.yMin = dataList[17];

                    }
                    return newData;
                }
            }
            catch (System.Exception)
            {
                Warning("[Easy File Save] GetUnityRect error using key: " + key);
            }
            return defaultValue;
        }

        #endregion


        #region " STATIC FUNCTIONS "

        private static List<string> filesArchive = new List<string>();

        public static void Register(string fileName)
        {
            if (!filesArchive.Contains(fileName)) filesArchive.Add(fileName);
        }

        /// <summary>
        /// Delete all the files created by Easy File Save.
        /// </summary>
        public static void DeleteAll()
        {
            foreach (string fileName in filesArchive)
            {
                if (File.Exists(fileName)) File.Delete(fileName);
            }
            filesArchive = new List<string>();
        }

        /// <summary>
        /// Serialize the object data in the proper way and return its XML structure.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize(object data)
        {
            System.Type st = data.GetType();

            var sw = new StringWriter();
            XmlSerializer ser = new XmlSerializer(st);
            ser.Serialize(sw, data);
            string xml = sw.ToString();

            return xml;
        }

        /// <summary>
        /// Deserialize the given data with the given type.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Deserialize(object data, System.Type type)
        {
            string xml = (string)data;

            XmlSerializer ser = new XmlSerializer(type);

            object result = null;
            using (TextReader reader = new StringReader(xml))
            {
                result = ser.Deserialize(reader);
            }

            return result;
        }

        #endregion


        #region " CONVERTERS "

        public class Converter
        {

            /// <summary>
            /// Cast the object into integer value.
            /// </summary>
            /// <returns></returns>
            public static int ToInt(object value)
            {
                try
                {
                    return (int)value;
                }
                catch (System.Exception)
                {
                    return 0;
                }
            }

            /// <summary>
            /// Cast the object into float value.
            /// </summary>
            /// <returns></returns>
            public static float ToFloat(object value)
            {
                try
                {
                    return (float)value;
                }
                catch (System.Exception)
                {
                    return 0f;
                }
            }

            /// <summary>
            /// Cast the object into a string.
            /// </summary>
            /// <returns></returns>
            public static string ToString(object value)
            {
                try
                {
                    return (string)value;
                }
                catch (System.Exception)
                {
                    return "";
                }
            }

            /// <summary>
            /// Cast the object into a boolean value.
            /// </summary>
            /// <returns></returns>
            public static bool ToBool(object value)
            {
                try
                {
                    return (bool)value;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

        }

        #endregion


    }
}


