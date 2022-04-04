namespace FUI_Studio.Classes.fui
{
    internal interface IFuiObject
    {
        int GetByteSize();
        string ToString();
        void Parse(byte[] data);
        byte[] ToArray();
    }
}