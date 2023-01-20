using System;
using System.Reflection;
using Newtonsoft.Json;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Common
{
  public class EnumNameConverter : JsonConverter
  {
    private ISet<Type> sets = new HashSet<Type>()
    {
      typeof(StockField),
      typeof(AccumulateField),
      typeof(AccumulatePeriod)
    };
    public EnumNameConverter()
    {
    }

    public override bool CanConvert(Type objectType)
    {
      throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
      if (value is null)
      {
        return;
      }
      Type curType = value.GetType();
      if (!sets.Contains(curType))
      {
        return;
      }
      
      foreach (FieldInfo field in curType.GetFields())
      {
        if (field.FieldType == curType && value == field.GetValue(null))
        {
          writer.WriteValue(field.Name);
        }
      }
    }
  }
}

