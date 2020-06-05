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

        public static void Add<T>(this ICollection<T> source, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                source.Add(item);
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
            using (var fileStream = File.OpenWrite(filePath))
            {
                stream.CopyTo(fileStream);
                fileStream.Flush();
            }
        }
    }
}
