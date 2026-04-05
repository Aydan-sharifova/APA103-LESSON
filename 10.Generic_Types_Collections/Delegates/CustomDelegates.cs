using System;
using _10.Generic_Types_Collections.Models;

namespace _10.Generic_Types_Collections.Delegates
{
        public delegate void MessageDelegate(string message);
        public delegate void BookDelegate(Book book);
        public delegate bool BookFilterDelegate(Book book);
        public delegate int BookIntDelegate(Book book);
    
}

