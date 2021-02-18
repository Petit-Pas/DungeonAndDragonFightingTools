﻿using DDFight.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Tools.Save
{
    public class PlayableEntityList<T> : GenericList<T> 
        where T : PlayableEntity, new()
    {
        public PlayableEntityList() : base()
        {
            this.ListChanged += NewPlayableEntityList_ListChanged;
            this.ListElementChanged += NewPlayableList_ListElementChanged;
        }

        private void NewPlayableList_ListElementChanged(object sender, ListElementChangedArgs e)
        {
            if (!Global.Loading)
                switch (e.Operation)
                {
                    case GenericListOperation.Addition:
                        NewSaveManager.SaveUnique<T>(e.Element);
                        break;
                    case GenericListOperation.Deletion:
                        NewSaveManager.DeleteUnique<T>(e.Element);
                        break;
                    case GenericListOperation.Modification:
                        NewSaveManager.SaveUnique<T>(e.Element);
                        break;
                    case GenericListOperation.Sort:
                        break;
                }
        }

        private void NewPlayableEntityList_ListChanged(object sender, ListChangedArgs e)
        {
            if (!Global.Loading)
            {
                switch (e.Operation)
                {
                    case GenericListOperation.Addition:
                        this.SortElements((x, y) => {
                            return x.Name.CompareTo(y.Name);
                        });
                        break;
                    case GenericListOperation.Deletion:
                        break;
                    case GenericListOperation.Modification:
                        break;
                    case GenericListOperation.Sort:
                        break;
                }
            }
        }
    }
}
