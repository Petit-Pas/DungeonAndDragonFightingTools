﻿namespace BaseToolsLibrary.Memory
{
    /// <summary>
    ///     Contains Metadata about execution
    /// </summary>
    public static class Global
    {
        // TODO , maybe adding a null protection in the getter would be enough as XML parse probably does not call the getter.
        public static bool Loading { get; set; } = true;
    }
}
