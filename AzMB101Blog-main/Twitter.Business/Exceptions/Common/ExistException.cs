using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entities.Common;

namespace Twitter.Business.Exceptions.Common
{
    public class ExistException<T> : Exception where T : BaseEntity
    {
        public ExistException() : base(typeof(T).Name + " already exist")
        {
        }

        public ExistException(string? message) : base(message)
        {
        }
    }
}
