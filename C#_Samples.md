# C# Samples

```Csharp

public List<List<StatementFutureDetail>> Months { get; set; } = new List<List<StatementFutureDetail>>();

.
.
.

private List<List<StatementFutureDetail>> Build(IList<StatementFutureLayoutDetail> list)
{
    var details = Convert(list);

    List<List<StatementFutureDetail>> months = details
        .GroupBy(det => det.OperationDate)
        .Select(group => group.ToList())
        .ToList();

    return months;
}
```

```Csharp

if (startDate != null && endDate != null )
{
    List<StatementFutureLayoutDetail> details = ApplingFilterByDates(startDate, endDate, statementFuture);
    statementFuture.Data.Detail = details;
}
else if (startDate != null && endDate == null)
{
    List<StatementFutureLayoutDetail> details = ApplingFilterByDates(startDate, DateTime.Now.AddDays(1), statementFuture);
    statementFuture.Data.Detail = details;
}

.
.
.

private List<StatementFutureLayoutDetail> ApplingFilterByDates(DateTime? startDate, DateTime? endDate, BaseResult<StatementFutureLayoutResponse> statementFuture)
{
    var from = ((DateTime)startDate).AddDays(-1);
    var to = ((DateTime)endDate).AddDays(1);

    return (
        from d in statementFuture.Data.Detail
        where startDate < GetDate(d.DataOperacao)
        where endDate > GetDate(d.DataOperacao)
        select d
    ).ToList();
}
```

```Csharp

public string GetTotal(List<StatementFutureDetail> list)
{
    var total = list.Sum(item => item.Value);
    return $"R$ {total.ToString("#,#.00#")}";
}
```

```Csharp

// http://jake.burgy.me/tidbits/2016/03/10/get-display-name-for-enum-value.html
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public static class EnumExtensions
{
    /// <summary>
    /// Retrieves the <see cref="DisplayAttribute.Name" /> property on the <see cref="DisplayAttribute" />
    /// of the current enum value, or the enum's member name if the <see cref="DisplayAttribute" /> is not present.
    /// </summary>
    /// <param name="val">This enum member to get the name for.</param>
    /// <returns>The <see cref="DisplayAttribute.Name" /> property on the <see cref="DisplayAttribute" /> attribute, if present.</returns>
    public static string GetDisplayName(this Enum val)
    {
        return val.GetType()
                  .GetMember(val.ToString())
                  .FirstOrDefault()
                  ?.GetCustomAttribute<DisplayAttribute>(false)
                  ?.Name
                  ?? val.ToString();
    }
}

.
.
.

public static class SegmentEnumExtension
{
    public static string GetIconName(this EstablishmentSegment segment)
    {
        var icon = $"/img/emporium-icons/ic-{segment.ToString().ToLower()}.svg";
        return icon;
    }

    public static string GetDisplayName(this EstablishmentSegment segment)
    {
        var attr = GetDisplayAttribute(segment);
        return attr != null ? attr.Name : segment.ToString();
        //return segment.GetDisplayName();
    }

    private static DisplayAttribute GetDisplayAttribute(object value)
    {
        Type type = value.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException(string.Format("Type {0} is not an enum", type));
        }
        var field = type.GetField(value.ToString());
        return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
    }
}


```

```Csharp
    public static class MaskUtils
    {
        public static string Strip(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return Regex.Replace(value, "[^0-9a-zA-Z]+", "");
        }

        public static string GetCpfCnpj(string cpf_cnpj)
        {
            if (IsCPF(cpf_cnpj))
            {
                return ConvertCPF(cpf_cnpj.Trim());
            }
            else if (IsCNPJ(cpf_cnpj))
            {
                return ConvertCNPJ(cpf_cnpj.Trim());
            }
            return "";
        }

        public static string GetCpfCnpj(string cpf, string cnpj)
        {
            if (IsCPF(cpf)) {
                string strippedValue = Strip(cpf.Trim());
                return ConvertCPF(strippedValue);
            }
            else if (IsCNPJ(cnpj))
            {
                string strippedValue = Strip(cnpj.Trim());
                return ConvertCNPJ(strippedValue);
            }
            return "";
        }

        public static string ConvertCPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static string ConvertCNPJ(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        private static bool IsCPF(string value)
        {
            if (String.IsNullOrWhiteSpace(value) || Strip(value).Length > 11) return false;
            return true;
        }

        private static bool IsCNPJ(string value)
        {
            if (String.IsNullOrWhiteSpace(value) || Strip(value).Length < 12) return false;
            return true;
        }
    }
```

```Csharp
public string GetSelected(string name1, string name2)
{
    if (RemoveDiacritics(name1.Trim()).Equals(RemoveDiacritics(name2.Trim()))) return "selected='selected'";
    return "";
}

static string RemoveDiacritics(string text)
{
    return string.Concat(
        text.Normalize(NormalizationForm.FormD)
        .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                      UnicodeCategory.NonSpacingMark)
      ).Normalize(NormalizationForm.FormC);
}
```
