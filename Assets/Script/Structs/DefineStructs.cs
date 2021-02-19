using System.Collections;
using System.Collections.Generic;

public enum CCGAssetType
{
    None,
    Money,
    Card,
    PackageItem
}

public class CCGAsset
{
    public CCGAssetType AssetType = CCGAssetType.None;
    public int Id;
    public int Count;
}
