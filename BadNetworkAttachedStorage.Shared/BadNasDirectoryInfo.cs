using System.IO.Library;
using System.Linq;

namespace System.IO {
    public class BadNasDirectoryInfo {
        private readonly DirectoryInfo DirectoryInfo;
        public BadNasDirectoryInfo(DirectoryInfo directoryInfo) => DirectoryInfo = directoryInfo;

        public BadNasFileInfo CreateFile(String fileName) =>
              RetryIO.Retry(() => {
                  DirectoryInfo.Refresh();
                  var badNasFileInfo = new BadNasFileInfo(new FileInfo(Path.Combine(DirectoryInfo.FullName, fileName)));
                  badNasFileInfo.Create();
                  return badNasFileInfo;
              });

        public void Create() =>
              RetryIO.Retry(() => {
                  DirectoryInfo.Refresh();
                  if (!DirectoryInfo.Exists)
                      DirectoryInfo.Create();
              });

        public BadNasDirectoryInfo CreateSubdirectory(String path) =>
            RetryIO.Retry(() => {
                DirectoryInfo.Refresh();
                return new BadNasDirectoryInfo(DirectoryInfo.CreateSubdirectory(path));
            });

        public BadNasDirectoryInfo[] GetDirectories() =>
             RetryIO.Retry(() => {
                 DirectoryInfo.Refresh();
                 return DirectoryInfo.GetDirectories()
                    .OrderBy(directoryInfo => directoryInfo.Name)
                        .Select(directoryInfo => new BadNasDirectoryInfo(directoryInfo))
                            .ToArray();
             });

        public BadNasDirectoryInfo[] GetDirectories(String searchPattern) =>
            RetryIO.Retry(() => {
                DirectoryInfo.Refresh();
                return DirectoryInfo.GetDirectories(searchPattern)
                   .OrderBy(directoryInfo => directoryInfo.Name)
                       .Select(directoryInfo => new BadNasDirectoryInfo(directoryInfo))
                           .ToArray();
            });

        public BadNasDirectoryInfo[] GetDirectories(String searchPattern, SearchOption searchOption) =>
            RetryIO.Retry(() => {
                DirectoryInfo.Refresh();
                return DirectoryInfo.GetDirectories(searchPattern, searchOption)
                   .OrderBy(directoryInfo => directoryInfo.Name)
                       .Select(directoryInfo => new BadNasDirectoryInfo(directoryInfo))
                           .ToArray();
            });

        public BadNasFileInfo[] GetFiles() =>
          RetryIO.Retry(() => {
              DirectoryInfo.Refresh();
              return DirectoryInfo.GetFiles()
                  .OrderBy(fileInfo => fileInfo.Name)
                      .Select(fileInfo => new BadNasFileInfo(fileInfo))
                          .ToArray();
          });

        public BadNasFileInfo[] GetFiles(String searchPattern, SearchOption searchOption) =>
          RetryIO.Retry(() => {
              DirectoryInfo.Refresh();
              return DirectoryInfo.GetFiles(searchPattern, searchOption)
                  .OrderBy(fileInfo => fileInfo.Name)
                      .Select(fileInfo => new BadNasFileInfo(fileInfo))
                          .ToArray();
          });

        public BadNasFileInfo[] GetFiles(String searchPattern) =>
          RetryIO.Retry(() => {
              DirectoryInfo.Refresh();
              return DirectoryInfo.GetFiles(searchPattern)
                  .OrderBy(fileInfo => fileInfo.Name)
                      .Select(fileInfo => new BadNasFileInfo(fileInfo))
                          .ToArray();
          });

        public void Delete() =>
            RetryIO.Retry(() => {
                DirectoryInfo.Refresh();
                DirectoryInfo.Delete();
            });

        public void Delete(Boolean recursive) =>
            RetryIO.Retry(() => {
                DirectoryInfo.Refresh();
                DirectoryInfo.Delete(recursive);
            });

        public String Name {
            get =>
                RetryIO.Retry(() => {
                    DirectoryInfo.Refresh();
                    return DirectoryInfo.Name;
                });
        }

        public String FullName {
            get =>
                RetryIO.Retry(() => {
                    DirectoryInfo.Refresh();
                    return DirectoryInfo.FullName;
                });
        }
    }
}
