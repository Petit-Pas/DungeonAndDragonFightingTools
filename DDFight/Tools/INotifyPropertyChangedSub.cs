using System.ComponentModel;

namespace DDFight.Tools
{
    //TODO not sure this is still useful anymore
    interface INotifyPropertyChangedSub
    {
        void PropertyChangedSubscript(PropertyChangedEventHandler handler);
    }
}
