﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using System.Windows.Input;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableElement(T)
    ///
    /// <summary>
    /// Represents a bindable element that has text, command, and value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableElement<T> : Bindable<T>, IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="gettext">Function to get text.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Getter<string> gettext, IDispatcher dispatcher) :
            this(default(T), gettext, dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="value">Initial value.</param>
        /// <param name="gettext">Function to get text.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(T value, Getter<string> gettext, IDispatcher dispatcher) :
            this(new Accessor<T>(value), gettext, dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="getter">Function to get value.</param>
        /// <param name="gettext">Function to get text.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Getter<T> getter, Getter<string> gettext, IDispatcher dispatcher) :
            this(new Accessor<T>(getter), gettext, dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="getter">Function to get value.</param>
        /// <param name="setter">Function to set value.</param>
        /// <param name="gettext">Function to get text.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Getter<T> getter, Setter<T> setter, Getter<string> gettext, IDispatcher dispatcher) :
            this(new Accessor<T>(getter, setter), gettext, dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="accessor">Function to get and set value.</param>
        /// <param name="gettext">Function to get text.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Accessor<T> accessor, Getter<string> gettext, IDispatcher dispatcher) :
            base(accessor, dispatcher)
        {
            _dispose = new OnceAction<bool>(Dispose);
            _gettext = gettext;
            _remover = Locale.Subscribe(e => Refresh(nameof(Text)));
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Gets the text.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Text => _gettext();

        /* ----------------------------------------------------------------- */
        ///
        /// Command
        ///
        /// <summary>
        /// Gets or sets the command object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ICommand Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ~BindableElement
        ///
        /// <summary>
        /// Finalizes the BindableElement.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~BindableElement() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases all resources used by the BindableElement.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            _dispose.Invoke(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by <c>BindableElement</c>
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
            if (disposing) _remover.Dispose();
        }

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        private readonly Getter<string> _gettext;
        private readonly IDisposable _remover;
        private ICommand _command;
        #endregion
    }
}
