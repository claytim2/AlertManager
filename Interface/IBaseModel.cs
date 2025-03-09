using System;

namespace Interface
{
    public interface IBaseModel
    {
        int Id { get; set; }
        int SiteId { get; set; }
        string UserCreate { get; set; }
        DateTime? DateCreate { get; set; }
        string UserUpdate { get; set; }
        DateTime? DateUpdate { get; set; }

    }
}
