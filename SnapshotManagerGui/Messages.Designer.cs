﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SnapshotManagerGui {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SnapshotManagerGui.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter a name for the new snapshot..
        /// </summary>
        internal static string EnterSnapshotName {
            get {
                return ResourceManager.GetString("EnterSnapshotName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sure?.
        /// </summary>
        internal static string SaveConnectionsFailedCaption {
            get {
                return ResourceManager.GetString("SaveConnectionsFailedCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The connections could not be saved ({0}).
        ///
        ///Would you like to close the application anyway?.
        /// </summary>
        internal static string SaveConnectionsFailedYesNoQuestion {
            get {
                return ResourceManager.GetString("SaveConnectionsFailedYesNoQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The snapshot has been successfully restored..
        /// </summary>
        internal static string SnapshotRestored {
            get {
                return ResourceManager.GetString("SnapshotRestored", resourceCulture);
            }
        }
    }
}
