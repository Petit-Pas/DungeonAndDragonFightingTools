using DDFight.Tools;
using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Controlers
{
    public class SpecializedListUserControl<T> : BaseListUserControl
        where T : class, IListable, new()
    {
        public SpecializedListUserControl() : base ()
        {
        }

        private GenericList<T> data_context
        {
            get => DataContext as GenericList<T>;
        }

        public override void edit(object obj)
        {
            data_context.EditElement(obj as T);
        }

        public override void remove(object obj)
        {
            data_context.RemoveElement(obj as T);
        }

        public override void duplicate(object obj)
        {
            data_context.DuplicateElement(obj as T);
        }

        public override void add_new(object obj = null)
        {
            data_context.AddElement(obj as T);
        }

    }
}
