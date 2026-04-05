using System;
namespace _10.Generic_Types_Collections.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() : base("Axtarılan kitab tapılmadı.") { }

        public ItemNotFoundException(string message) : base(message) { }
    }
}

