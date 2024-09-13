using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    private string persistentDataPath;
    private string gameDataPath;
    private string gameDataBackupPath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        persistentDataPath = Application.persistentDataPath;
        gameDataPath = Path.Combine(persistentDataPath, gameDataFile);
        gameDataBackupPath = gameDataPath + backupExtension;
    }

    private List<IDataPersistence> dataPersistingObjects; //Contains scripts extended to IDataPersistence
    [HideInInspector] public GameData gameData; //Contains scripts that are Serializable
    private readonly string gameDataFile = "game.dat"; //The file containing the GameData
    private readonly string backupExtension = ".bak"; //The file extension for the backup version of game.dat
    public bool useGameData = true; //Play Scenes without loading or saving gameData if false
    [SerializeField] private bool useEncryption = false;
    private readonly string encryptionCodeWord = "johncentventura";

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded; //Subscribes here on OnEnable
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded; //Unsubscribes here on OnDisable

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Find all objects of type MonoBehaviour & IDataPersistence, using System.Linq will grant access for using .OfType<>();
        dataPersistingObjects = new List<IDataPersistence>(FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>());
    }

    public void NewGame()
    {
        gameData = new GameData();
        SaveGame(); //Creates a file with initialize value from ScriptableObjects
        LoadGame(); //Reads the file with initialize value
    }

    public void LoadGame()
    {
        gameData = LoadDataFile(); //Loads a file containing a gameData

        if (gameData == null) return;

        //Iterates dataPersistenceObjects so each of its object calls its LoadData() using this gameData
        foreach (IDataPersistence dataPersistenceObject in dataPersistingObjects) dataPersistenceObject.LoadData(gameData);
    }

    public void SaveGame()
    {
        if (gameData == null) return;

        //Iterates dataPersistenceObjects so each of its object calls its SaveData() using this gameData
        foreach (IDataPersistence dataPersistenceObject in dataPersistingObjects) dataPersistenceObject.SaveData(gameData);

        SaveDataFile(gameData);
    }

    #region File Handler
    private GameData LoadDataFile(bool useBackup = true)
    {
        GameData loadedGameData = null;

        if (File.Exists(gameDataPath)) //If a file exists in the path
        {
            try
            {
                string fileData = ""; //Placeholder for the deserialized data from a file

                //use using() when reading or writing a file, to ensure the connection to a file is closed after reading or writing it
                using (FileStream stream = new FileStream(gameDataPath, FileMode.Open)) //FileMode.Open reads the file
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        fileData = reader.ReadToEnd(); //ReadToEnd() reads the file and return it as a string
                    }
                }

                if (useEncryption) fileData = EncryptAndDecrypt(fileData);

                loadedGameData = JsonUtility.FromJson<GameData>(fileData); //Deserialize data of fileData (JSON to C#)
            }
            catch (Exception e) //Use the file backup version
            {
                if (useBackup)
                {
                    Debug.LogWarning("Failed to load file, attempting to load backup file\n" + e);
                    if (IsDataBackupSuccessful(gameDataPath)) loadedGameData = LoadDataFile(false);
                    else Debug.LogError("Failed to load file & backup file at: " + gameDataPath + "\n" + e);
                }
            }
        }

        if (loadedGameData == null) Debug.Log("No data can be loaded");

        return loadedGameData;
    }

    private void SaveDataFile(GameData gameData)
    {
        if (gameData == null) Debug.Log("No data can be saved");

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(gameDataPath)); //Create or get a directory where the file will be saved

            string dataFile = JsonUtility.ToJson(gameData, true); //Placeholder for the serialized data from GameData

            if (useEncryption) dataFile = EncryptAndDecrypt(dataFile);

            //use using() when reading or writing a file, to ensure the connection to a file is closed after reading or writing it
            using (FileStream stream = new FileStream(gameDataPath, FileMode.Create)) //FileMode.Open reads the file
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataFile); //Writes dataFile to a file, creates the file if it doesn't exist
                }
            }

            //LoadDataFile() is called to check if the file is working so we can create a backup copy of the file
            if (LoadDataFile() != null) File.Copy(gameDataPath, gameDataBackupPath, true);
            else throw new Exception("Failed to verify save file, backup file cannot be created at: " + gameDataBackupPath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save file at: " + gameDataPath + "\n" + e);
        }
    }

    private bool IsDataBackupSuccessful(string dataPath)
    {
        bool success = false;
        string gameDataBackupPath = dataPath + backupExtension;

        try
        {
            if (File.Exists(gameDataBackupPath)) //If a backup file exists in the path
            {
                //Creates an original copy of a file from the backup file
                File.Copy(gameDataBackupPath, dataPath, true);
                success = true;
            }
            else throw new Exception("No existing backup file.");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to backup at path: " + gameDataBackupPath + "\n" + e);
        }

        return success;
    }

    private string EncryptAndDecrypt(string data) //Simple XOR Encryption
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }

        return modifiedData;
    }
    #endregion
}