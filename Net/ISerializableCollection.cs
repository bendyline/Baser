namespace Bendyline.Base
{
    public interface ISerializableCollection 
    {
        void Clear();
        SerializableObject Create();
        void Add(SerializableObject o);
    }
}
