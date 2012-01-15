using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookReader.Models
{
    public class TestData 
    {
        public static void Seed(BookReaderContext context)
        {
            var quran = new BookModel
                {
                    Id = Guid.NewGuid(),
                    Title = "The Holy Quran",
                    Author = "Muhammad",
                    FilePath = @"C:\Projects\BookReader\BookReader\Files\Source\001_Quran.txt"
                };

            context.BookModels.Add(quran);
            context.SaveChanges();
            Utilities.Import.ImportBook(quran.FilePath, quran);

            var gems = new BookModel
            {
                Id = Guid.NewGuid(),
                Title = "Gems of Divine Mysteries",
                Author = "Baha'u'llah",
                FilePath = @"C:\Projects\BookReader\BookReader\Files\Source\002_Gems.txt"
            };

            context.BookModels.Add(gems);
            context.SaveChanges();
            Utilities.Import.ImportBook(gems.FilePath, gems);

        }
    }
}