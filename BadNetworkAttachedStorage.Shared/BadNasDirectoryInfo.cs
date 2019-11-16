using System.IO.Library;
using System.Linq;

namespace System.IO {
    public class BadNasDirectoryInfo {
        private readonly DirectoryInfo DirectoryInfo;
        public BadNasDirectoryInfo(DirectoryInfo directoryInfo) => DirectoryInfo = directoryInfo;

        public BadNasFileInfo[] GetFiles() =>
            RetryIO.Retry(() => {
                DirectoryInfo.Refresh();
                return DirectoryInfo.GetFiles()
                    .OrderBy(fileInfo => fileInfo.Name)
                        .Select(fileInfo => new BadNasFileInfo(fileInfo))
                            .ToArray();
            });

        public BadNasDirectoryInfo[] GetDirectories() =>
             RetryIO.Retry(() => {
                 DirectoryInfo.Refresh();
                 return DirectoryInfo.GetDirectories()
                    .OrderBy(directoryInfo => directoryInfo.Name)
                        .Select(directoryInfo => new BadNasDirectoryInfo(directoryInfo))
                            .ToArray();
             });
    }
}
