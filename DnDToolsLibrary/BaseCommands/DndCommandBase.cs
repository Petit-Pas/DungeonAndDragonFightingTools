using System;
using System.Collections.Generic;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.BaseCommands
{
    public abstract class DndCommandBase : IMediatorCommand
    {
        private static readonly Lazy<ICustomConsole> _console = new(DIContainer.GetImplementation<ICustomConsole>);
        protected static ICustomConsole FightConsole => _console.Value;

        public int AddLog(string text)
        {
            return AddLog(text, FightConsole.DefaultFontWeight);
        }

        public int AddLog(string text, IFontWeight fontWeight)
        {
            return AddLog(text, fontWeight, FightConsole.DefaultFontColor);
        }

        public int AddLog(string text, IFontWeight fontWeight, IFontColor fontColor)
        {
            return AddLog(text, fontWeight, fontColor, FightConsole.DefaultFontSize);
        }

        public int AddLog(string text, IFontWeight fontWeight, IFontColor fontColor, int fontSize)
        {
            var hash = FightConsole.AddEntry(text, fontWeight, fontColor, fontSize);
            _logMessages.Add(hash);
            return hash;
        }

        public int AddLogAfter(int previousHash, string text)
        {
            return AddLogAfter(previousHash, text, FightConsole.DefaultFontWeight);
        }

        public int AddLogAfter(int previousHash, string text, IFontWeight fontWeight)
        {
            return AddLogAfter(previousHash, text, fontWeight, FightConsole.DefaultFontColor);
        }

        public int AddLogAfter(int previousHash, string text, IFontWeight fontWeight, IFontColor fontColor)
        {
            return AddLogAfter(previousHash, text, fontWeight, fontColor, FightConsole.DefaultFontSize);
        }

        public int AddLogAfter(int previousHash, string text, IFontWeight fontWeight, IFontColor fontColor, int fontSize)
        {
            var hash = FightConsole.AddEntryAfter(previousHash, text, fontWeight, fontColor, fontSize);
            _logMessages.Add(hash);
            return hash;
        }


        public void AddLog(int entryHash)
        {
            _logMessages.Add(entryHash);
        }

        private List<int> _logMessages = new ();

        public void ClearLogs()
        {
            _logMessages.Clear();
        }

        public IEnumerable<int> GetLogHashes()
        {
            return _logMessages;
        }

        public IMediatorCommandResponse Cancel()
        {
            FightConsole.RemoveEntries(GetLogHashes());
            ClearLogs();
            return MediatorCommandStatii.Canceled;
        }
    }
}
