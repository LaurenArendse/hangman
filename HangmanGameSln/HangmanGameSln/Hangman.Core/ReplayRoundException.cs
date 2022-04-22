using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Core
{
    public class ReplayRoundException : Exception
    {
  
        public ReplayRoundException(string message) : base(message)
        {

        }
       
    }
}
