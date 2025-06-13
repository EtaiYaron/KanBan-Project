using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response<T>
    {
        private string errorMessage;
        private T? returnValue;

        public Response() { }

        public Response(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
        public Response(string errorMessage, T returnValue)
        {
            this.errorMessage = errorMessage;
            this.returnValue = returnValue;
        }

        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set { this.errorMessage = value; }
        }

        public T ReturnValue
        {
            get { return this.returnValue; }
            set { this.returnValue = value; }
        }
    }
}
