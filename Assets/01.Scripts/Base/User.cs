using System.Collections.Generic;

[System.Serializable]
public class User
{
    public int stage;
    public int maxStage;

    public bool IsTutorial(int stage)
    {
        return (stage >= maxStage);
    }
}
