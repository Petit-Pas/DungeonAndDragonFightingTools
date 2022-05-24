using System;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Status.StatusCommands.ApplyDotCommands
{
    public class ApplyDotCommand : SuperDndCommandBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusReference"></param>
        /// <param name="startOfTurn"></param>
        /// <param name="casterTurn"></param>
        public ApplyDotCommand(Guid statusReference, bool startOfTurn, bool casterTurn)
        {
            CasterTurn = casterTurn;
            StatusReference = statusReference;
            StartOfTurn = startOfTurn;
        }

        public Guid StatusReference { get; }
        public bool StartOfTurn { get; }
        public bool CasterTurn { get; }

    }
}
