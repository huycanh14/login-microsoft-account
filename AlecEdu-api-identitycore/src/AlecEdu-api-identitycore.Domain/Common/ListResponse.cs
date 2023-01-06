namespace AlecEdu_api.Domain.Common;

public partial class ListResponse<T>
{
    public virtual IEnumerable<T> ListData { get; set; }
    public virtual IEnumerable<HeaderTableVm>? ListHeader { get; set; }
    public virtual int Count { get; set; }

    public ListResponse()
    {
        ListData = new List<T> { };
        ListHeader = new List<HeaderTableVm> { };
        Count = 0;
    }

    public ListResponse(IEnumerable<T> data, IEnumerable<HeaderTableVm>? listHeader, int count = 0)
    {
        ListData = data;
        ListHeader = listHeader;
        Count = count;
    }
}