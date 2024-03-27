
[System.Serializable]
public class PlayerData
{
    public float posX;
    public float posY;
    public float posZ;
    public float rotX;
    public float rotY;
    public float rotZ;

    public PlayerData(float x,float y, float z, float rotX, float rotY, float rotZ)
    {
        posX = x;
        posY = y;
        posZ = z;
        this.rotX = rotX;
        this.rotY = rotY;
        this.rotZ = rotZ;
    }
}

