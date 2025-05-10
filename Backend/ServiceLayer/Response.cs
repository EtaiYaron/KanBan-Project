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

        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set { this.errorMessage = value; }
        }

        public object ReturnValue
        {
            get { return this.returnValue; }
            set { this.returnValue = value; }
        }
    }
}
