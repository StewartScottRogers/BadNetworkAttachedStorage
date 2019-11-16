using System.IO.Library;
using System.Linq;
using System.Text;

namespace System.IO {
    public class BadNasFileInfo {
        private readonly FileInfo FileInfo;

        public BadNasFileInfo(FileInfo fileInfo) => FileInfo = fileInfo;

        public Boolean IsReadOnly {
            get =>
                RetryIO.Retry(() => {
                    FileInfo.Refresh();
                    return FileInfo.IsReadOnly;
                });
        }

        public void Create() =>
          RetryIO.Retry(() => {
              FileInfo.Refresh();
              if (!FileInfo.Exists)
                  FileInfo.Create();
          });

        public Boolean Exists() =>
            RetryIO.Retry(() => {
                FileInfo.Refresh();
                return FileInfo.Exists;
            });

        public String Name {
            get =>
                RetryIO.Retry(() => {
                    FileInfo.Refresh();
                    return FileInfo.Name;
                });
        }

        public String FullName {
            get =>
                RetryIO.Retry(() => {
                    FileInfo.Refresh();
                    return FileInfo.FullName;
                });
        }


        public void AppendAllText(String contents) =>
            RetryIO.Retry(() => {
                FileInfo.Refresh();
                File.AppendAllText(FileInfo.FullName, contents);
            });

        public void AppendAllText(String contents, Encoding encoding) =>
            RetryIO.Retry(() => {
                FileInfo.Refresh();
                File.AppendAllText(FileInfo.FullName, contents, encoding);
            });

        public void WriteAllText(String contents) =>
           RetryIO.Retry(() => {
               FileInfo.Refresh();
               File.WriteAllText(FileInfo.FullName, contents);
           });

        public void WriteAllText(String contents, Encoding encoding) =>
            RetryIO.Retry(() => {
                FileInfo.Refresh();
                File.WriteAllText(FileInfo.FullName, contents, encoding);
            });



        public String ReadAllText() =>
              RetryIO.Retry(() => {
                  FileInfo.Refresh();
                  return File.ReadAllText(FileInfo.FullName);
              });

        public String ReadAllText(Encoding encoding) =>
              RetryIO.Retry(() => {
                  FileInfo.Refresh();
                  return File.ReadAllText(FileInfo.FullName, encoding);
              });

        public String[] ReadLines() =>
              RetryIO.Retry(() => {
                  FileInfo.Refresh();
                  return File.ReadLines(FileInfo.FullName).ToArray();
              });

        public String[] ReadLines(Encoding encoding) =>
              RetryIO.Retry(() => {
                  FileInfo.Refresh();
                  return File.ReadLines(FileInfo.FullName, encoding).ToArray();
              });
    }
}
