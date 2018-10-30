﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace MQTTDataProvider.Model
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This class encapsulates the INotifyPropertyChanged implementation and provides helper methods
    /// to the derived class so that they can easily trigger the appropriate notifications.
    /// </summary>
    ///
    /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class BindableBase : INotifyPropertyChanged
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets a property. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="member">       [in,out] The member. </param>
        /// <param name="val">          The value. </param>
        /// <param name="propertyName"> (Optional) Name of the property. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected virtual void SetProperty<T>(ref T member, T val,
         [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Executes the property changed action. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="propertyName"> Name of the property. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        /// <summary>   Occurs when a property value changes. </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}