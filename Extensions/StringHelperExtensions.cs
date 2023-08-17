using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace BizStream.Migrations.Extensions
{
    public static class StringHelperExtensions
    {
        public static string CleanHTML( string text )
        {
            var document = new HtmlDocument();
            document.LoadHtml( text );

            return WebUtility.HtmlDecode( document.DocumentNode.InnerText );
        }

        public static string GetFirstHtmlHeading( string html )
        {
            for (int i = 1; i <= 5; i++)
            {
                var headingOpen = $"<h{i}";
                var headingClose = $"</h{i}>";

                var startIndex = html.IndexOf( headingOpen );

                if (startIndex != -1)
                {
                    var endIndex = html.IndexOf( $"</h{i}>", startIndex + headingOpen.Length + 1);
                    if (endIndex != -1)
                    {
                        var headingHtml = html.Substring( startIndex, endIndex + headingClose.Length );
                        return headingHtml;
                    }
                }
            }

            return string.Empty;
        }
    }
}
