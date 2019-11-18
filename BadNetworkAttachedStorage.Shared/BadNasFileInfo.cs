using System.IO.Library;
using System.Linq;
using System.Text;

namespace System.IO {
    public class BadNasFileInfo {
        private readonly FileInfo FileInfo;

        public BadNasFileInfo(FileInfo fileInfo) => FileInfo = fileInfo;

        public void Create() =>
          RetryIO.Retry(() => {
              FileInfo.Refresh();
              if (!FileInfo.Exists)
                  FileInfo.Create();
          });

        public Boolean Exists() =>
            RetryIO.Retry(() => {
                return File.Exists(FileInfo.FullName);
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
                File.AppendAllText(FileInfo.FullName, contents);
            });

        public void AppendAllText(String contents, Encoding encoding) =>
            RetryIO.Retry(() => {
                File.AppendAllText(FileInfo.FullName, contents, encoding);
            });

        public void WriteAllText(String contents) =>
           RetryIO.Retry(() => {
               File.WriteAllText(FileInfo.FullName, contents);
           });

        public void WriteAllText(String contents, Encoding encoding) =>
            RetryIO.Retry(() => {
                File.WriteAllText(FileInfo.FullName, contents, encoding);
            });



        public String ReadAllText() =>
              RetryIO.Retry(() => {
                  return File.ReadAllText(FileInfo.FullName);
              });

        public String ReadAllText(Encoding encoding) =>
              RetryIO.Retry(() => {
                  return File.ReadAllText(FileInfo.FullName, encoding);
              });

        public String[] ReadLines() =>
              RetryIO.Retry(() => {
                  return File.ReadLines(FileInfo.FullName).ToArray();
              });

        public String[] ReadLines(Encoding encoding) =>
              RetryIO.Retry(() => {
                  return File.ReadLines(FileInfo.FullName, encoding).ToArray();
              });

        public override String ToString() => FileInfo.FullName;
    }
}
