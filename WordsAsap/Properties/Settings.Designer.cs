﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WordsAsap.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AccentColor {
            get {
                return ((string)(this["AccentColor"]));
            }
            set {
                this["AccentColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Uri ThemeSource {
            get {
                return ((global::System.Uri)(this["ThemeSource"]));
            }
            set {
                this["ThemeSource"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Large")]
        public global::FirstFloor.ModernUI.Presentation.FontSize SelectedFontSize {
            get {
                return ((global::FirstFloor.ModernUI.Presentation.FontSize)(this["SelectedFontSize"]));
            }
            set {
                this["SelectedFontSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public int WordDialogShowInterval {
            get {
                return ((int)(this["WordDialogShowInterval"]));
            }
            set {
                this["WordDialogShowInterval"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("WordsCollection.db3")]
        public string WordsCollectionStorageFile {
            get {
                return ((string)(this["WordsCollectionStorageFile"]));
            }
            set {
                this["WordsCollectionStorageFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int MaxNumberOfWordDisplays {
            get {
                return ((int)(this["MaxNumberOfWordDisplays"]));
            }
            set {
                this["MaxNumberOfWordDisplays"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowWordInPopupDialog {
            get {
                return ((bool)(this["ShowWordInPopupDialog"]));
            }
            set {
                this["ShowWordInPopupDialog"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WordsCollectionStorageFolder {
            get {
                return ((string)(this["WordsCollectionStorageFolder"]));
            }
            set {
                this["WordsCollectionStorageFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.5")]
        public double BalloonTipTransparency {
            get {
                return ((double)(this["BalloonTipTransparency"]));
            }
            set {
                this["BalloonTipTransparency"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double DialogPositionTop {
            get {
                return ((double)(this["DialogPositionTop"]));
            }
            set {
                this["DialogPositionTop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double DialogPositionLeft {
            get {
                return ((double)(this["DialogPositionLeft"]));
            }
            set {
                this["DialogPositionLeft"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double DialogSizeHeight {
            get {
                return ((double)(this["DialogSizeHeight"]));
            }
            set {
                this["DialogSizeHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double DialogSizeWidth {
            get {
                return ((double)(this["DialogSizeWidth"]));
            }
            set {
                this["DialogSizeWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BalloonTipGradientOff {
            get {
                return ((bool)(this["BalloonTipGradientOff"]));
            }
            set {
                this["BalloonTipGradientOff"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AddWordConfirmation {
            get {
                return ((bool)(this["AddWordConfirmation"]));
            }
            set {
                this["AddWordConfirmation"] = value;
            }
        }
    }
}
