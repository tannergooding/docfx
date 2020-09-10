// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Docs.Build
{
    internal abstract class ErrorBuilder
    {
        public static readonly ErrorBuilder Null = new NullErrorBuilder();

        public abstract bool HasError { get; }

        public abstract void Add(Error error);

        public abstract bool FileHasError(FilePath file);

        public void AddIfNotNull(Error? error)
        {
            if (error != null)
            {
                Add(error);
            }
        }

        public void AddRange(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Add(error);
            }
        }

        public void AddRange(IEnumerable<DocfxException> exceptions)
        {
            foreach (var exception in exceptions)
            {
                Log.Write(exception);
                Add(exception.Error);
            }
        }

        private class NullErrorBuilder : ErrorBuilder
        {
            public override bool HasError => throw new NotSupportedException();

            public override void Add(Error error) { }

            public override bool FileHasError(FilePath file) => throw new NotSupportedException();
        }
    }
}