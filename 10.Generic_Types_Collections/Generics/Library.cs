using System;
using _10.Generic_Types_Collections.Exceptions;

namespace _10.Generic_Types_Collections.Generics
{
    public class Library<T>
    {
        public List<T> Items { get; set; }
        public string Name { get; set; }

        public Library(string name)
        {
            Name = name;
            Items = new List<T>();
        }

        public void Add(T item)
        {
            Items.Add(item);
            Console.WriteLine("Kitab əlavə edildi");
        }

        public void Remove(T item)
        {
            bool removed = Items.Remove(item);

            if (!removed)
            {
                throw new ItemNotFoundException("Silinmək istənilən kitab tapılmadı.");
            }

            Console.WriteLine("Kitab silindi");
        }

        public List<T> GetAll()
        {
            return Items;
        }

        public int Count()
        {
            return Items.Count;
        }

        public T FindByIndex(int index)
        {
            if (index < 0 || index >= Items.Count)
            {
                throw new InvalidIndexException($"İndeks aralıqdan kənardır: {index}");
            }

            return Items[index];
        }
    }
}

