using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTriviaConsumer
{
    /// <summary>
    /// Represents an Open Trivia
    /// category
    /// </summary>
    public class TriviaCategory
    {
        public int id { get; set; }

        public string name { get; set; }

    }

    public class CategoryResponse
    {
        public List<TriviaCategory> trivia_categories { get; set; }
    }
}
