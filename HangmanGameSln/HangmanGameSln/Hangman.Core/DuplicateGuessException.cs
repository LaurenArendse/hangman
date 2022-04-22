using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Core
{
    public class DuplicateGuessException : Exception
    {
        public DuplicateGuessException(string message) : base(message)
        {

        }
    }
}
