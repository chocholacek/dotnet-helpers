namespace DotnetHelperTool.Helpers;

public enum DataSizeUnit : ulong
{
    B = 1,
    KB = 1024 * B,
    MB = 1024 * KB,
    GB = 1024 * MB,
    TB = 1024 * GB, 
}


public static class DataSizeHelper
{
    public static double Convert(double value, DataSizeUnit from, DataSizeUnit to) => value * ((double)from / (double)(to));
}