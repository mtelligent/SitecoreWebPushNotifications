using Toast.Foundation.ORM.Models;

namespace Toast.Foundation.Content.Tests.Models
{
    public interface ITestItem : IGlassBase
    {
        string Title { get; set; }
    }
}
