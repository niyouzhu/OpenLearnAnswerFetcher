using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class Extensions
    {

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}

namespace System.IO
{

    public static class Extensions
    {
        public static void SaveAsFile(this Stream stream, string filePath)
        {
            using(var fileStream = File.OpenWrite(filePath))
            {
                stream.CopyTo(fileStream);
                fileStream.Flush();
            }
        }
    }
}
