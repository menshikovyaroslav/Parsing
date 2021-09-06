using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baucenter.ru
{
    /// <summary>
    /// Класс карточка товара.
    /// Метод Parse - парсинг HTML, запись свойств Title и Price
    /// </summary>
    public class Card
    {
        public string Price { get; set; }
        public string Title { get; set; }

        public void Parse(string html)
        {
            var priceStart = html.IndexOf("Цена") + 11;
            var priceEnd = html.IndexOf("<span", priceStart);
            Price = html.Substring(priceStart, priceEnd - priceStart).Trim();

            var titleStart = html.IndexOf("<h1>") + 4;
            var titleEnd = html.IndexOf("</h1>", titleStart);
            Title = html.Substring(titleStart, titleEnd - titleStart).Trim();
        }
    }
}
