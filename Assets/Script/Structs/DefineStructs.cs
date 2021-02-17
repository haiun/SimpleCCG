using System.Collections;
using System.Collections.Generic;

public enum AssetType
{
    None,
    Money,
    Card,
    PackageItem
}

public class Asset
{
    public AssetType AssetType = AssetType.None;
    public int Id;
    public int Count;
}
