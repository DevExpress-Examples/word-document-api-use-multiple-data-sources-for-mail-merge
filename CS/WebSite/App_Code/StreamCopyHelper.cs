using System.IO;

public static class StreamCopyHelper {
    public static void Copy(Stream src, Stream dst) {
        const int bufferSize = 32768;
        byte[] buffer = new byte[bufferSize];
        int bytesRead = 0;
        do {
            bytesRead = src.Read(buffer, 0, bufferSize);
            dst.Write(buffer, 0, bytesRead);

        }
        while (bytesRead == bufferSize);
    }
}