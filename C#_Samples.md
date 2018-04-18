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
