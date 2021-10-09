﻿using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands
{
    public class TryApplyStatusCommand : SuperCommandBase
    {
        /// <summary>
        ///     Will try to apply a status on a given target
        ///     
        ///     Here the savingThrow must be provided only when it was not linked to the saving especially
        ///     Example: 
        ///     - Synaptic Static has a saving throw for damage AND status, hence, the saving is not linked to the status
        ///     - An attack that can cause bleed with a saving throw for the bleeding is not concerned by the saving, the saving is linked to the status
        /// </summary>
        /// <param name="status"> The status to apply </param>
        /// <param name="saving"> The saving implied with the status. </param>
        /// <param name="caster"> The entity that applies the status </param>
        /// <param name="target"> The entity that is affected by the status </param>
        public TryApplyStatusCommand(string casterName, string targetName, OnHitStatus status, SavingThrow saving = null)
        {
            Status = status;
            Saving = saving;
            CasterName = casterName;
            TargetName = targetName;
        }

        public OnHitStatus Status { get; }
        public SavingThrow Saving { get; set; }
        public string CasterName { get; }
        public string TargetName { get; }
    }
}