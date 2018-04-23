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
