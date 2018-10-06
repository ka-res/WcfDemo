using System;

namespace WcfDemo
{
    public class BaseModel
    {
        public DateTime SaveDate { get; set; }

        public bool IsSoftDeleted { get; set; }
    }
}
