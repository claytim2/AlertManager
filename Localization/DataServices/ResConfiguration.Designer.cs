﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Localization.DataServices {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResConfiguration {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResConfiguration() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Localization.DataServices.ResConfiguration", typeof(ResConfiguration).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Idioma do Aplicação.
        /// </summary>
        public static string CFG_ApplicationCulture {
            get {
                return ResourceManager.GetString("CFG_ApplicationCulture", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Caminho Executavel.
        /// </summary>
        public static string CFG_ApplicationPath {
            get {
                return ResourceManager.GetString("CFG_ApplicationPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Número de dias para manter de LOG.
        /// </summary>
        public static string CFG_ClearLogDays {
            get {
                return ResourceManager.GetString("CFG_ClearLogDays", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Idioma do Log.
        /// </summary>
        public static string CFG_LogCulture {
            get {
                return ResourceManager.GetString("CFG_LogCulture", resourceCulture);
            }
        }
    }
}
