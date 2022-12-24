using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace MessageHandlingSystem.Infrastructure.DataAccess.ValueConverters;

public class DictionaryConverter<TKey, TValue> : ValueConverter<IReadOnlyDictionary<TKey, TValue>, string>
where TKey : notnull
{
    public DictionaryConverter()
        : base(
            x => ConvertTo(x),
            x => ConvertFrom(x))
    {
    }

    private static string ConvertTo(IReadOnlyDictionary<TKey, TValue> readOnlyDictionary)
    {
        return JsonConvert.SerializeObject(readOnlyDictionary);
    }

    private static IReadOnlyDictionary<TKey, TValue> ConvertFrom(string s)
    {
        return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(s) ?? throw new NotImplementedException();
    }
}