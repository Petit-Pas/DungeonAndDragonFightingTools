using DDFight.Controlers;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression.Attacks.Display
{
    public class HitAttackTemplateListUserControl : BaseListUserControl
    {
        public HitAttackTemplateListUserControl() : base()
        {
            DataContextChanged += HitAttackTemplateListUserControl_DataContextChanged;
        }

        private ObservableCollection<HitAttackTemplate> data_context
        {
            get => DataContext as ObservableCollection<HitAttackTemplate>;
        }

        private void HitAttackTemplateListUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            EntityList = data_context;
        }

        #region ListControl
        public override void edit(object obj)
        {
            HitAttackTemplate attack = obj as HitAttackTemplate;
            attack.Edit();
        }

        public override void remove(object obj)
        {
            data_context.Remove(obj as HitAttackTemplate);
        }

        public override void duplicate(object obj)
        {
            HitAttackTemplate new_template = ((HitAttackTemplate)obj).Clone() as HitAttackTemplate;
            new_template.Name = new_template.Name + " - Copy";
            add_new(new_template);
        }

        public override void add_new(object obj = null)
        {
            HitAttackTemplate attack = obj as HitAttackTemplate;
            if (attack == null)
                attack = new HitAttackTemplate();
            if (attack.Edit())
                data_context.Add(attack);
        }

        #endregion ListControl

    }
}
