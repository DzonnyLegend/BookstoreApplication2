using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClientFront.Models
{
    [DataContract]
    public class BookViewModel
    {
        [DataMember]
        public long BookId { get; set; }
        [DataMember]
        public string? Title { get; set; }
        [DataMember]
        public string? Author { get; set; }
        [DataMember]
        public double? Price { get; set; }
        [DataMember]
        public string? Genre { get; set; }
        [DataMember]
        public uint Quantity { get; set; }
        [DataMember]
        public string? Description { get; set; }
        [DataMember]
        public uint Count { get; set; } = 1;

        public BookViewModel(long bookId, string? bookTitle, string? description, string? author, double? price, uint quantity)
        {
            BookId = bookId;
            Title = bookTitle;
            Description = description;
            Author = author;
            Price = price;
            Quantity = quantity;
        }

        public BookViewModel() { }

        public override string? ToString()
        {
            return base.ToString();
        }

    }
}

