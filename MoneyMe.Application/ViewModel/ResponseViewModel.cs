using MoneyMe.Application.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMe.Application.ViewModel
{
    public class ResponseViewModel<T>
    {
        public Status Status { get; set; }

        public string Message { get; set; }

        public T Value { get; set; }
    }
}
