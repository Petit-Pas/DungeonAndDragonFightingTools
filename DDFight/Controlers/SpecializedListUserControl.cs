using DnDToolsLibrary.Memory;
using System;
using WpfToolsLibrary.Display;

namespace DDFight.Controlers
{
    public class SpecializedListUserControl<T> : BaseListUserControl
        where T : class, ICloneable, new()
    {
        public SpecializedListUserControl() : base ()
        {
        }

        private GenericList<T> data_context
        {
            get => DataContext as GenericList<T>;
        }

        public override bool edit(object obj)
        {
            Console.WriteLine("WARNING: edit method should be overriden in SpecializedListUserControl");
            return false;
        }

        public override void remove(object obj)
        {
            data_context.RemoveElement(obj as T);
        }

        public override void duplicate(object obj)
        {
            if (obj is T instance)
            {
                T new_one = instance.Clone() as T;
                if (instance is INameable)
                {
                    ((INameable)new_one).Name = ((INameable)new_one).Name + " - Copy";
                }
                add_new(new_one);
            }
        }

        public override void add_new(object obj = null)
        {
            if (obj == null)
                obj = new T();
            if (obj is T instance)
            {
                if (edit(instance))
                    data_context.AddElementSilent(instance);
                else
                {
                    if (instance is IDisposable disposable)
                        disposable.Dispose();
                }
            }
        }
    }
}
