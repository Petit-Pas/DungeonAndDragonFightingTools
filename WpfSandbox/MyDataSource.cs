// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BindValidation
{
    public class MyDataSource
    {
        public MyDataSource(string text)
        {
            Text = text;
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => _text = value;
        }
    }
}