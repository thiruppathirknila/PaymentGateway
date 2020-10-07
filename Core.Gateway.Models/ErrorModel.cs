using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Models
{
    public class ErrorModel
    {
        public List<ValidationFailedMsg> validationFailedMsg { get; set; }
        public List<string> errors { get; set; }
    }
    public class ValidationFailedMsg
    {
        public string Key { get; set; }

        public string Message { get; set; }

    }
}
