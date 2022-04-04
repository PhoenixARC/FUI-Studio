using System;

namespace FUI_Studio.Classes.fui
{
    public class FuiBitmap : fui.IFuiObject
    {
        public int symbolIndex;
        public eFuiBitmapType format;
        public int width;
        public int height;
        public int offset;
        public int size;
        public int zlib_data_size;
        public int __0x1C;

        public enum eFuiBitmapType
        {
            PNG_WITH_ALPHA_DATA = 1, //! fully ignored
            PNG_NO_ALPHA_DATA = 3,   //! fully ignored
            JPEG_NO_ALPHA_DATA = 6,
            JPEG_UNKNOWN = 7, //! TODO: find name
            JPEG_WITH_ALPHA_DATA = 8
        }

        public int GetByteSize()
        {
            return 0x20;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data is null");
            if (data.Length != GetByteSize()) throw new ArgumentException();
            symbolIndex = BitConverter.ToInt32(data, 0);
            format = (eFuiBitmapType)BitConverter.ToInt32(data, 4);
            width = BitConverter.ToInt32(data, 8);
            height = BitConverter.ToInt32(data, 12);
            offset = BitConverter.ToInt32(data, 16);
            size = BitConverter.ToInt32(data, 20);
            zlib_data_size = BitConverter.ToInt32(data, 24);
            __0x1C = BitConverter.ToInt32(data, 28);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(symbolIndex).CopyTo(arr, 0);
            BitConverter.GetBytes((int)format).CopyTo(arr, 4);
            BitConverter.GetBytes(width).CopyTo(arr, 8);
            BitConverter.GetBytes(height).CopyTo(arr, 12);
            BitConverter.GetBytes(offset).CopyTo(arr, 16);
            BitConverter.GetBytes(size).CopyTo(arr, 20);
            BitConverter.GetBytes(zlib_data_size).CopyTo(arr, 24);
            BitConverter.GetBytes(__0x1C).CopyTo(arr, 28);
            return arr;
        }

        public override string ToString()
        {
            return $"Symbol Index: {symbolIndex}\n" +
                $"Format: {format}\n" +
                $"Size: {width}x{height}\n" +
                $"Image Offset: {offset}\n" +
                $"Image Size: {size}\n" +
                $"Zlib data offset: {zlib_data_size}\n";
        }
    }
}
