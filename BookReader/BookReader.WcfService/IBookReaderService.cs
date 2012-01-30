using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BookReader.Data.Models;
using System.Runtime.Serialization;

namespace BookReader.WcfService
{
    [ServiceContract]
    public interface IBookReaderService
    {
        [OperationContract]
        List<Book> GetBookList();

        [OperationContract]
        List<Chapter> GetChapterList(Guid bookId);

        [OperationContract]
        List<Verse> GetVerseList(Guid chapterId);
    }

}
