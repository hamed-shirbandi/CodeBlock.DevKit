﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeBlock.DevKit.Core.Resources {
    
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
    public class CommonResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public CommonResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CodeBlock.DevKit.Core.Resources.CommonResource", typeof(CommonResource).Assembly);
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
        ///   Looks up a localized string similar to {0} قبلا ثبت شده است.
        /// </summary>
        public static string ALready_Exists {
            get {
                return ResourceManager.GetString("ALready_Exists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to طول {0} میبایست بین {1} تا {2} کارکتر باشد.
        /// </summary>
        public static string Length_Error {
            get {
                return ResourceManager.GetString("Length_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to طول {0} نمیتواند بیشتر از {1} کارکتر باشد.
        /// </summary>
        public static string Max_Length_Error {
            get {
                return ResourceManager.GetString("Max_Length_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to طول {0} نمیتواند کمتر از {1} کارکتر باشد.
        /// </summary>
        public static string Min_Length_Error {
            get {
                return ResourceManager.GetString("Min_Length_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} پیدا نشد.
        /// </summary>
        public static string Not_Found {
            get {
                return ResourceManager.GetString("Not_Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} را وارد کنید.
        /// </summary>
        public static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to یک خطای ناشناخته رخ داد.
        /// </summary>
        public static string UnknownExceptionHappened {
            get {
                return ResourceManager.GetString("UnknownExceptionHappened", resourceCulture);
            }
        }
    }
}
