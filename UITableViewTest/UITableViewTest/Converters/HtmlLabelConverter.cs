using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace UITableViewTest.Converters
{
    public class HtmlLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var formatted = new FormattedString();

            foreach (var item in ProcessString((string)value))
                formatted.Spans.Add(CreateSpan(item));

            return formatted;
        }

        private Span CreateSpan(StringSection section)
        {
            var span = new Span()
            {
                Text = section.Text
            };

            if (!string.IsNullOrEmpty(section.Link))
            {
                span.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = _navigationCommand,
                    CommandParameter = section.Link
                });
                span.TextColor = Color.Blue;
                // Underline coming soon from https://github.com/xamarin/Xamarin.Forms/pull/2221
                // Currently available in Nightly builds if you wanted to try, it does work :)
                // As of 2018-07-22. But not avail in 3.2.0-pre1.
                // span.TextDecorations = TextDecorations.Underline;
            }

            return span;
        }

        public IList<StringSection> ProcessString(string rawText)
        {
            const string spanPattern = @"(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+";

            MatchCollection collection = Regex.Matches(rawText, spanPattern, RegexOptions.None);
            var sections = new List<StringSection>();

            foreach (Match item in collection)
            {
                var lastIndex = 0;
                var foundText = item.Value;
                var tt = rawText.Substring(lastIndex);
                var index = tt.IndexOf(foundText);
                if (lastIndex != index)
                {
                    var tmpSS = new StringSection() { Text = rawText.Substring(lastIndex, index - lastIndex) };
                    sections.Add(tmpSS);
                    lastIndex = lastIndex + item.Length + index;
                }
                var ss = new StringSection() { Text = rawText.Substring(index, item.Length), Link = item.Value };

                sections.Add(ss);
                rawText = rawText.Substring(lastIndex);
            }

            return sections;
        }

        public class StringSection
        {
            public string Text { get; set; }
            public string Link { get; set; }
        }

        private ICommand _navigationCommand = new Command<string>((url) =>
        {
            var uriBuilder = new UriBuilder(url)
            {
                Scheme = Uri.UriSchemeHttps,
                Port = -1
            };
            Device.OpenUri(uriBuilder.Uri);
            
        });

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
