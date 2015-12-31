using NLog.Config;
using NLog.Layouts;

namespace NLog.ServiceBus
{
    [NLogConfigurationItem]
    [ThreadAgnostic]
    public class KeyLayoutAttribute
    {
        [RequiredParameter]
        public string Key { get; set; }

        [RequiredParameter]
        public Layout Layout { get; set; }


        public KeyLayoutAttribute(string key, Layout layout)
        {
            Key = key;
            Layout = layout;
        }

        public static KeyLayoutAttribute Create(string key, Layout layout)
        {
            return new KeyLayoutAttribute(key, layout);
        }
    }
}