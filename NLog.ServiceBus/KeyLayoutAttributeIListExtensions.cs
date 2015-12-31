using System.Collections.Generic;
using System.Linq;

namespace NLog.ServiceBus
{
    internal static class KeyLayoutAttributeIListExtensions
    {
        internal static IEnumerable<KeyValuePair<string, string>> Render(this IList<KeyLayoutAttribute> list,
            LogEventInfo logEvent)
        {
            return list
                .Select(x => new KeyValuePair<string, string>(x.Key, x.Layout.Render(logEvent)))
                .ToArray();
        }
    }
}