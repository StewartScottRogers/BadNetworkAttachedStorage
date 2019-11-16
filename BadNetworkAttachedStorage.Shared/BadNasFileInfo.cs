using System.IO.Library;

namespace System.IO {
    public class BadNasFileInfo {
        private readonly FileInfo FileInfo;

        public BadNasFileInfo(FileInfo fileInfo) => FileInfo = fileInfo;

        public Boolean Exists() =>
             RetryIO.Retry(() => {
                 FileInfo.Refresh();
                 return FileInfo.Exists;
             });
    }

}
