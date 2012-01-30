using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookReader.Data.Models
{
    public class TestData 
    {
        public static void Seed(BookReaderContext context)
        {
            var quran = new Book
                {
                    Title = "The Holy Quran",
                    Author = "Muhammad",
                    FilePath = @"C:\Projects\BookReader\BookReader\Files\Source\001_Quran_test.txt"
                };

            context.Books.Add(quran);
            context.SaveChanges();
            Utilities.Import.ImportBook(quran);

            var gems = new Book
            {
                Title = "Gems of Divine Mysteries",
                Author = "Baha'u'llah",
                FilePath = @"C:\Projects\BookReader\BookReader\Files\Source\002_Gems.txt"
            };

            context.Books.Add(gems);
            context.SaveChanges();
            Utilities.Import.ImportBook(gems);

        }
    }
}