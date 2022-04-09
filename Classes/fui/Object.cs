namespace FUI_Studio.Classes.fui
{
    public enum eFuiObjectType : byte
    {
        STAGE = 0,
        SHAPE = 1,
        TIMELINE = 2,
        BITMAP = 3,
        REFERENCE = 4,
        EDITTEXT = 5,
        CODEGENRECT = 6,
    }
    internal interface IFuiObject
    {
        int GetByteSize();
        string ToString();
        void Parse(byte[] data);
        byte[] ToArray();
    }
}