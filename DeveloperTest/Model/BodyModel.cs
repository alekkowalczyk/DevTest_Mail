using DeveloperTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Model
{
    class BodyModel
    {
        public string Content { get; private set; }
        public BodyType Type { get; private set; }

        private BodyModel(string content, BodyType type)
        {
            Content = content;
            Type = type;
        }

        public static BodyModel GetTextBody(string text)
        {
            return new BodyModel(text, BodyType.TXT);
        }

        public static BodyModel GetHtmlBody(string html)
        {
            return new BodyModel(html, BodyType.HTML);
        }
    }
}
