namespace ShadowTester.Util
{
    public class DataStorageUnitHelper
    {
        private const int BYTES_PER_KILOBYTES = 1024;
        private const int KILOBYTES_PER_MEGABYTES = 1024;
        private const int MEGABYTES_PER_GIGABYTE = 1024;

        public static long BytesToKbs(long bytes)
        {
            return bytes / BYTES_PER_KILOBYTES;
        }

        public static long BytesToMbs(long bytes)
        {
            return BytesToKbs(bytes) / KILOBYTES_PER_MEGABYTES;
        }

        public static long BytesToGbs(long bytes)
        {
            return BytesToMbs(bytes) / MEGABYTES_PER_GIGABYTE;
        }
    }
}