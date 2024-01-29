using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace StockFlow.Web.Utilities;

public static class ApiVersions
{
    public const string V1 = "1.0";
    public const string V2 = "2.0";

    public static List<string> GetVersions()
    {
        var result = new List<string>();
        FieldInfo[] fields = typeof(ApiVersions).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

        foreach (FieldInfo field in fields)
        {
            if (field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string) && field.Name.StartsWith('V'))
            {
                // Console.WriteLine(field.Name + " = " + field.GetRawConstantValue());
                var versionValue = field.GetRawConstantValue()!.ToString()!;
                string majorVersion = versionValue.Split('.')[0];
                string docVersion = "v" + majorVersion;
                if (!result.Contains(docVersion))
                {
                    result.Add(docVersion);
                }
            }
        }
        
        return result;
    }
}