using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public class Statistics<T>
    {
        public T Object { get; set; }
        public int NumberOfOrders { get; set; }
        public string ObjectName { get; set; }
        public List<Statistics<Cake>> OrderedTogetherCakes { get; set; } = new List<Statistics<Cake>>();
        public List<Statistics<Cream>> OrderedTogetherCreams { get; set; } = new List<Statistics<Cream>>();
        public List<Statistics<Additional>> OrderedTogetherAdditionals { get; set; } = new List<Statistics<Additional>>();
        public List<Statistics<Decoration>> OrderedTogetherDecorations { get; set; } = new List<Statistics<Decoration>>();
    }
}
