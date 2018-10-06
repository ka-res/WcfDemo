using System;

namespace WcfDemo
{
    public class BaseModel : IBaseModel
    {
        public DateTime SaveDate { get; set; }

        public bool IsSoftDeleted { get; set; }
    }

    public interface IBaseModel
    {
        DateTime SaveDate { get; set; }

        bool IsSoftDeleted { get; set; }
    }
}
