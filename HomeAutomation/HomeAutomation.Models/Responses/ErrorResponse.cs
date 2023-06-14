using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Models.Responses
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}
