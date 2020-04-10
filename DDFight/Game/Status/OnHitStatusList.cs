using DDFight.Game.Status.Display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Status
{
    public class OnHitStatusList : INotifyPropertyChanged, ICloneable
    {
        public OnHitStatusList() { }

        public ObservableCollection<OnHitStatus> List
        {
            get => _list;
            set
            {
                _list = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<OnHitStatus> _list = new ObservableCollection<OnHitStatus>();

        /*public void OpenEditWindow(PlayableEntity Owner)
        {
            OnHitStatusListEditWindow window = new OnHitStatusListEditWindow();
            PlayableEntity dc = (PlayableEntity)Owner.Clone();
            dc.Validated = false;

            window.DataContext = dc;

            window.ShowDialog();

            if (dc.Validated == true)
                List = dc.OnHitStatusList.List;
        }*/

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public void init_copy(OnHitStatusList to_copy)
        {
            List = to_copy.List.Clone();
        }

        public OnHitStatusList(OnHitStatusList to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new OnHitStatusList(this);
        }

        public virtual void CopyAssign(object _to_copy)
        {
            OnHitStatusList to_copy = (OnHitStatusList)_to_copy;
            init_copy(to_copy);
        }

    }
}
