namespace System.IO {
    public class BadNasFileInfo {
        private readonly FileInfo FileInfo;

        public BadNasFileInfo(FileInfo fileInfo) {
            FileInfo = fileInfo;
        }

        public Boolean Exists() {
            FileInfo.Refresh();
            return FileInfo.Exists;
        }
    }

}
