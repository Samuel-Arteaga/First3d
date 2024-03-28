using System.IO;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private Transform playerTransform;
    //Ruta relativa del Data.json
    private string saveFilePath = "./Assets/Scripts/Player/Data.json"; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("data"))
        {
            SavePlayerPosition();
            SoundManager.Instance.Guardado();
        }
    }
    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        LoadPlayerPosition();
    }
    public void SavePlayerPosition()
    {
        PlayerData playerData = new PlayerData(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z, playerTransform.rotation.x, playerTransform.rotation.y, playerTransform.rotation.z);
        string jsonData = JsonUtility.ToJson(playerData);
        try
        {
            using (StreamWriter writer = new StreamWriter(saveFilePath))
            {
                writer.Write(jsonData);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al guardar la posición: " + e.Message);
        }
    }
    public void LoadPlayerPosition()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                using (StreamReader reader = File.OpenText(saveFilePath)) 
                {
                    string jsonData = reader.ReadToEnd();
                    PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                    playerTransform.position = new Vector3(playerData.posX, playerData.posY, playerData.posZ);
                }
            }
            else
            {
                Debug.LogWarning("No se encontró el archivo en: " + saveFilePath);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al cargar la posición: " + e.Message);
        }
    }
}

