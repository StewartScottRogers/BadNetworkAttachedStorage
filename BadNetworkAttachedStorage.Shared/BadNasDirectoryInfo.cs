namespace System.IO {
    public class BadNasDirectoryInfo {
        private readonly DirectoryInfo DirectoryInfo;
        public BadNasDirectoryInfo(DirectoryInfo directoryInfo) {
            DirectoryInfo = directoryInfo;
        }

        public FileInfo[] GetFiles() {
            DirectoryInfo.Refresh();
            return DirectoryInfo.GetFiles();
        }

        public DirectoryInfo[] GetDirectories() {
            DirectoryInfo.Refresh();
            return DirectoryInfo.GetDirectories();
        }
    }
}
