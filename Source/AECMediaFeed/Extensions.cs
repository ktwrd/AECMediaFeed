/*
   Copyright 2025 Kate Ward <kate@dariox.club>

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

namespace AECMediaFeed;

internal static class Extensions
{
    public static TResult ParseXml<TResult>(this MemoryStream stream)
        where TResult : class, new()
    {
        var ser = new XmlSerializer(typeof(TResult));
        var data = ser.Deserialize(stream);
        if (data is TResult x)
            return x;
        return new TResult();
    }

    public static T GetDefaultValue<T>()
        where T : struct, Enum
    {
        return (T)GetDefaultValue(typeof(T));
    }

    public static object? GetDefaultValue(Type enumType)
    {
        var attribute = enumType.GetCustomAttribute<DefaultValueAttribute>(inherit: false);
        if (attribute != null)
            return attribute.Value;

        var innerType = enumType.GetEnumUnderlyingType();
        var zero = Activator.CreateInstance(innerType);
        if (zero != null && enumType.IsEnumDefined(zero))
            return zero;

        var values = enumType.GetEnumValues();
        return values.GetValue(0);
    }

    public static TEnum[] GetFlags<TEnum>(this TEnum instance, bool ignoreZero = false)
        where TEnum : struct, Enum
    {
        var result = new List<TEnum>();
        foreach (var i in Enum.GetValues<TEnum>().Where(x => instance.HasFlag(x)))
        {
            if (ignoreZero && Enum.Equals(i, GetDefaultValue<TEnum>()))
            {
                continue;
            }
            result.Add(i);
        }
        return result.Distinct().ToArray();
    }
}
