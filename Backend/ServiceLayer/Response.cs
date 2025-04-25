using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response
    {
        private string errorMessage;
        private object returnValue;

        public Response() { }

        public Response(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
        public Response(string errorMessage, object returnValue)
        {
            this.errorMessage = errorMessage;
            this.returnValue = returnValue;
        }

        public string ErrorMessage { get; set; }

        public object ReturnValue { get; set; }
    }
}
