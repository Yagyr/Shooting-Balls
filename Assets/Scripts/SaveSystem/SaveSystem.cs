using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// TODO: Почитай что такое JSON, разберись с ним и переделай на него
public static class SaveSystem 
{
    public static void Save(Progress progress)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        // TODO: Чтобы не плодить название файла в разных местах вынеси этот путь в переменную или метод 
        // TODO: И вообще старайся не плодить копипасту - если видишь, что код повторяется, выноси его в отдельную сущность
        string path = Application.persistentDataPath + "/progress.yo";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        ProgressData progressData = new ProgressData(progress);
        binaryFormatter.Serialize(fileStream, progressData);
        fileStream.Close();
    }

    public static ProgressData Load()
    {
        string path = Application.persistentDataPath + "/progress.yo";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            ProgressData progressData = binaryFormatter.Deserialize(fileStream) as ProgressData;
            fileStream.Close();
            return progressData;
        }
        else
        {
            Debug.Log("No File");
            return null;
        }
    }

    public static void DeleteData()
    {
        File.Delete(Application.persistentDataPath + "/progress.yo");
    }
}
