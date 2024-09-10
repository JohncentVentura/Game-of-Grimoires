using System;

[Serializable]
public enum ECardProps
{
    MainType, CardName, ManaType, SubType
}

[Serializable]
public class Prop
{
    public string name;
    public string value;

    public Prop(string value)
    {
        this.value = value;
    }
}