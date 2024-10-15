using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace sapr.Utilities
{
    public static class TryParseClass
    {
        public static bool TryParseJson<T>(this string @this, out T result)
        {
            bool success = true;
            result = default;

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true,
                    ReadCommentHandling = JsonCommentHandling.Skip
                };

                // Попытка десериализации
                var tempResult = System.Text.Json.JsonSerializer.Deserialize<T>(@this, options);

                // Дополнительная проверка: если тип десериализованного объекта не соответствует ожидаемому
                if (tempResult == null || !IsJsonMatchingType(@this, typeof(T)))
                {
                    success = false;
                }
                else
                {
                    result = tempResult;
                }
            }
            catch (System.Text.Json.JsonException)
            {
                success = false;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        private static bool IsJsonMatchingType(string json, Type targetType)
        {
            try
            {
                // Попытка прочитать JSON в виде словаря для проверки свойств
                using (var doc = JsonDocument.Parse(json))
                {
                    var root = doc.RootElement;
                    foreach (var property in targetType.GetProperties())
                    {
                        if (!root.TryGetProperty(property.Name, out _))
                        {
                            return false; // Если свойство отсутствует в JSON, возвращаем false
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false; // Если что-то пошло не так при парсинге, возвращаем false
            }
        }
    }
}
