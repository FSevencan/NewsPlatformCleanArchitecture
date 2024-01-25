using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Application.Utilities;
public static class Slug
{
    private static readonly Dictionary<char, string> TurkishCharacterMap = new Dictionary<char, string>
    {
        { 'ç', "c" },
        { 'ğ', "g" },
        { 'ı', "i" },
        { 'ö', "o" },
        { 'ş', "s" },
        { 'ü', "u" },
        { 'Ç', "c" },
        { 'Ğ', "g" },
        { 'İ', "i" },
        { 'Ö', "o" },
        { 'Ş', "s" },
        { 'Ü', "u" }
    };

    public static string CreateSlug(string title)
    {
        var modifiedTitle = title.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in modifiedTitle)
        {
            if (TurkishCharacterMap.TryGetValue(c, out var replacement))
            {
                stringBuilder.Append(replacement);
            }
            else if (char.IsLetterOrDigit(c) || c == ' ')
            {
                stringBuilder.Append(c);
            }
        }

        var slug = stringBuilder.ToString().ToLowerInvariant();

       
        slug = Regex.Replace(slug, @"\s+", "-"); // Boşlukları tire ile değiştir
        slug = Regex.Replace(slug, @"\-{2,}", "-"); // Ardışık tireleri tek tire ile değiştir

        return slug.Trim('-'); // Başında ve sonunda kalan tireleri temizle
    }
}

